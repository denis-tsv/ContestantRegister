    using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Cqrs.Features._Common.Commands;
using ContestantRegister.Cqrs.Features._Common.Queries;
using ContestantRegister.Domain;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Utils;
using Microsoft.AspNetCore.Mvc;
using AutoFilter;

namespace ContestantRegister.Controllers._Common
{
    //мда, этот класс побил мой предыдущий личный рекорд по числу генерик-параметров :) https://youtu.be/HvSaBVSAkz4?t=1705
    //но было интересно, как будет выглядеть cqrs в crud сценарии
    //TODO если будет не лень, сделать вариант реализации просто через генерики, будет на порядок проще 
    public abstract class CrudController<TKey,
        TEntity, TListItemViewModel, TCreateViewModel, TEditViewModel,
        TGetEntitiesQuery, TGetEntityByIdQuery, TGetEntityByIdForDeleteQuery,
        TCreateEntityCommand, TEditEntityCommand, TDeleteEntityByIdCommand> 
            : Controller
        where TKey : IEquatable<TKey>
        where TEntity : IHasId<TKey>
        where TCreateViewModel : class
        where TEditViewModel : class

        where TGetEntitiesQuery : GetMappedEntitiesQuery<TEntity, TListItemViewModel>
        where TGetEntityByIdQuery : GetEntityByIdQuery<TEntity, TKey>, new()
        where TGetEntityByIdForDeleteQuery : GetEntityByIdForDeleteQuery<TEntity, TKey>, new()

        where TCreateEntityCommand : CreateMappedEntityCommand<TEntity, TCreateViewModel>, new()
        where TEditEntityCommand : EditMappedEntityCommand<TEntity, TEditViewModel, TKey>, new()
        where TDeleteEntityByIdCommand : DeleteEntityByIdCommand<TEntity, TKey>, new()
    {
        protected readonly IHandlerDispatcher HandlerDispatcher;
        private readonly IMapper _mapper;

        protected CrudController(IHandlerDispatcher dispatcher, IMapper mapper)
        {
            HandlerDispatcher = dispatcher;
            _mapper = mapper;
        }

        // GET: Entities
        public async Task<IActionResult> Index(TGetEntitiesQuery query)
        {
            MapFilterToViewData(query);

            var entities = await HandlerDispatcher.ExecuteQueryAsync(query);

            return View(entities);
        }

        // GET: Entities/Create
        public async Task<IActionResult> Create()
        {
            await FillViewDataForCreateAsync();

            var viewModel = BuildCreateViewModel();

            return View(viewModel);
        }

        protected virtual TCreateViewModel BuildCreateViewModel()
        {
            return null;
        }

        // POST: Entities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await FillViewDataForCreateAsync(viewModel);
                return View(viewModel);
            }

            try
            {
                //TODO можно заменить фабричным методом
                var command = new TCreateEntityCommand {Entity = viewModel};
                InitCreateCommand(command);

                await HandlerDispatcher.ExecuteCommandAsync(command);
            }
            catch (ValidationException e)
            {
                e.ValidationResult.ForEach(res => ModelState.AddModelError(res.Key, res.Value));
                await FillViewDataForCreateAsync(viewModel);
                return View(viewModel);
            }
            //TODO можно обобщить исключение
            catch (UnableToCreateUserException e)
            {
                ModelState.AddErrors(e.Errors);
                await FillViewDataForCreateAsync(viewModel);
                return View(viewModel);
            }
            
            return RedirectToAction(nameof(Index));
        }

        protected virtual void InitCreateCommand(TCreateEntityCommand command)
        {

        }

        // GET: Entities/Edit/5
        public async Task<IActionResult> Edit(TKey id)
        {
            if (id == null) throw new EntityNotFoundException();

            var entity = await HandlerDispatcher.ExecuteQueryAsync(new TGetEntityByIdQuery
            {
                Id = id,
                IncludeProperties = GetIncludePropertiesForEdit()
            });
            if (entity == null) throw new EntityNotFoundException();

            var viewModel = _mapper.Map<TEditViewModel>(entity);

            await FillViewDataForEditAsync(viewModel);

            return View(viewModel);
        }

        
        protected virtual string[] GetIncludePropertiesForEdit()
        {
            return null;
        }

        // POST: Entities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TKey id, TEditViewModel viewModel)
        {
            //if (id != entity.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await HandlerDispatcher.ExecuteCommandAsync(new TEditEntityCommand
                    {
                        Entity = viewModel,
                        Id = id,
                    });
                }
                catch (ValidationException e)
                {
                    e.ValidationResult.ForEach(res => ModelState.AddModelError(res.Key, res.Value));
                    await FillViewDataForEditAsync(viewModel);
                    return View(viewModel);
                }

                return RedirectToAction(nameof(Index));
            }

            await FillViewDataForEditAsync(viewModel);

            return View(viewModel);
        }

        // GET: Entities/Delete/5
        public async Task<IActionResult> Delete(TKey id)
        {
            if (id == null) throw new EntityNotFoundException();

            var entity = await HandlerDispatcher.ExecuteQueryAsync(new TGetEntityByIdForDeleteQuery
            {
                Id = id,
                IncludeProperties = GetIncludePropertiesForDelete()
            });
            if (entity == null) throw new EntityNotFoundException();

            return View(entity);
        }

        protected virtual string[] GetIncludePropertiesForDelete()
        {
            return null;
        }

        // POST: Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(TKey id)
        {
            await HandlerDispatcher.ExecuteCommandAsync(new TDeleteEntityByIdCommand { Id = id });

            return RedirectToAction(nameof(Index));
        }

        protected void MapFilterToViewData(TGetEntitiesQuery filter)
        {
            var properties = TypeInfoCache
                .GetPublicProperties(filter.GetType())
                .Select(x => new
                {
                    x.Name,
                    Value = x.GetValue(filter)
                })
                .Where(x => x.Value != null);

            foreach (var property in properties)
            {
                ViewData[property.Name] = property.Value;
            }
        }

        protected virtual Task FillViewDataForEditAsync(TEditViewModel viewModel = null)
        {
            return Task.FromResult(0);
        }

        protected virtual Task FillViewDataForCreateAsync(TCreateViewModel viewModel = null)
        {
            return Task.FromResult(0);
        }


    }
}
