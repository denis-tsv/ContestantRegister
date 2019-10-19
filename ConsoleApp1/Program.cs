//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using ContestantRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//using ContestantRegister.Cqrs.Features.Admin.Users.Queries;
//using ContestantRegister.Cqrs.Features.Admin.Users.ViewModels;
//using ContestantRegister.Framework.Ddd;
//using ContestantRegister.Framework.Filter;
//using Microsoft.EntityFrameworkCore;
//using ContestantRegister.DataAccess;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //TODO написать нормальные тесты 
            //var context = new ApplicationDbContext();
            //var area = new Area { Name = "Test Delete" };
            //context.Add(area);
            //context.SaveChanges();

            //var c1 = new ApplicationDbContext();
            //var ar = new Area {Id = area.Id};
            //c1.Remove(ar);
            //c1.SaveChanges();

            //var deq = context.Areas.Find(10);

            //var s1 = new Spec<Area>(x => x.Id < 5);
            //var s2 = new Spec<Area>(x => x.Id < 5);
            //Expression<Func<Area, bool>> e1 = (x) => x.Id < 5;
            //Expression<Func<Area, bool>> e2 = (x) => x.Id < 5;
            //var b = s1 == s2;
            //var b1 = e1 == e2;

            //var s3 = s1 && s2;
            //var r1 = context.Areas.Where(s1).ToList();
            //var r2 = context.Areas.Where(s2).ToList();
            //var r3 = context.Areas.Where(s3).ToList();


            //var u = context

            //.Cities
            //.SingleOrDefault(x => x.Id == 5);
            //.Include(x => x.StudyPlace)
            // .Users
            //.Where(x=> x.City, new Spec<City>(x => ids.Contains(x.Id)))
            //.Include(b => b.ContestRegistrationsParticipant1)
            //.Where(x => x.ContestRegistrationsParticipant1.Any(r => r.IsOutOfCompetition))
            //.WhereAny(x => x.ContestRegistrationsParticipant1, new Spec<ContestRegistration>(r => ids.Contains(r.ContestId)))
            //.Where(x => x.StudyPlace.ShortName.Contains("СФУ"))
            //    .Select(x => new { x.Id, x.FirstName, x.Email })                
            //    .ToList()
            //    ;
            //Console.Out.Flush();
            //int t = 0;  

            //context.Contests.Where(x => !x.IsArchive).ToList();
            //context.Contests.Where(x => x.IsArchive).ToList();
            //
            //context.Contests.Where(Contest.Archived).ToList();

            //var res1 = context.Users
            //    .Where(x => x.Email.Contains("P"))
            //    .ToList();

            //var res2 = context.Users
            //    .Where(x => x.Email.Contains("p"))
            //    .ToList();

            //var res = context.Users
            //    .Where(x => x.Email.ToLower().Contains("P".ToLower()))
            //    .ToList();

            //var l1 = context.Users
            //    .Where(x => EF.Functions.Like(x.Email, "P%"))
            //    .ToList();

            //var l11 = context.Users
            //    .Where(x => EF.Functions.Like(x.Email, "p%"))
            //    .ToList();

            //var l2 = context.Users
            //    .Where(x => EF.Functions.ILike(x.Email, "P%"))
            //    .ToList();

            //var l3 = context.Users
            //    .Where(x => EF.Functions.ILike(x.Email, "p%"))
            //    .ToList();

            //int t = 0;

            //var filter = new GetUsersQuery
            //{
            //    UserTypeName = "тре",
            //    //City = "крас"
            //};

            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<ApplicationUser, UserListItemViewModel>()
            //    .ForMember(x => x.StudyPlace, opt => opt.MapFrom(y => y.StudyPlace.ShortName))
            //    .ForMember(x => x.City, opt => opt.MapFrom(y => y.StudyPlace.City.Name))                
            //    );

            //var flt = context.Users
            //    //.Where(ApplicationUser.LockoutEnabledSpec.To)
            //    .ProjectTo<UserListItemViewModel>(configuration)
            //    .AutoFilter(filter)
            //    .ToList();
            //Console.Out.Flush();

            //int t1 = 0;

            //var city1 = new City { Name = "Абакан" };
            //var city2 = new City { Name = "Красноярск" };

            //var sp1 = new Institution { City = city2, ShortName = "СФУ" };
            //var sp2 = new Institution { City = city1, ShortName = "ХГУ" };
            //var sp3 = new Institution { ShortName = "НГИИ" };

            //var users = new List<ApplicationUser>()
            //{
            //    new ApplicationUser
            //    {
            //        Id = "1",
            //        StudyPlace = sp1,
            //        UserType = UserType.Pupil
            //    },
            //    new ApplicationUser
            //    {
            //        Id = "2",
            //        StudyPlace = sp2,
            //        UserType = UserType.Pupil
            //    },
            //    new ApplicationUser
            //    {
            //        Id = "3",
            //        StudyPlace = sp3,
            //        UserType = UserType.Student
            //    },
            //    new ApplicationUser
            //    {
            //        Id = "4",
            //        UserType = UserType.Trainer
            //    },
            //};

            //var qwe = users.MyWhere(x => x.StudyPlace != null && x.StudyPlace.City != null && x.StudyPlace.City.Name.Contains("ан"));



            //var context = new ApplicationDbContext();

            //IQueryable<ApplicationUser> users = context.Users
            ////.Include(c => c.StudyPlace)
            //.Include(c => c.StudyPlace.City)
            //;

            //var parameter = Expression.Parameter(typeof(ApplicationUser));
            //var prop = Expression.Property(parameter, "StudyPlace");
            //var prop1 = Expression.Property(prop, "ShortName");
            //var fvalExpression = Expression.Constant("ИГУ");
            //var eq = Expression.Equal(prop1, fvalExpression);
            //var resexpr = Expression.Lambda<Func<ApplicationUser, bool>>(eq, parameter);

            //var userExpr = Expression.Parameter(typeof(ApplicationUser));
            //var studPlace = Expression.Property(userExpr, "StudyPlace");
            //var city = Expression.Property(studPlace, "City");
            //var name = Expression.Property(city, "Name");
            //var fvalExpression = Expression.Constant("Красноярск");
            //var eq = Expression.Equal(name, fvalExpression);
            //var resexpr = Expression.Lambda<Func<ApplicationUser, bool>>(eq, userExpr);

            //var filtered = users
            //.Where(user => user.Name.Contains("Ир"))
            //.Where(user => user.StudyPlace.ShortName == "ИГУ")
            //.Where(user => user.StudyPlace.ShortName.IndexOf("ИГУ", StringComparison.OrdinalIgnoreCase) != -1)
            //.Where(user => user.StudyPlace.City.Name == "Красноярск")
            //.Where(user => user.StudyPlace.City.Name.IndexOf("абакан", StringComparison.OrdinalIgnoreCase) != -1)
            //.Where(resexpr)
            ;
            //var filtered = users.AutoFilter(filter);



            //var res = filtered.ToList();

            //int t = 0;

        }


    }

    static class Test
    {
        public static IQueryable<TSource> MyWhere<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.Where(predicate);
        }

        public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.Where(predicate.Compile());
        }
    }
}
