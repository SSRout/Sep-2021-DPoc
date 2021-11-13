using DPoc.Efcore;
using DPoc.Efcore.Entities;
using DPoc.Efcore.Repositories;
using EntityFrameworkCore.Testing.Moq;
using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnotTech.VideoApplication2021.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Xunit;

namespace Infrastructure.DA.Test.Repositories
{
    public class GenreRepositoryTest
    {
        private readonly VideoApplicationDbContext _fackeContext;
        public GenreRepositoryTest()
        {
            _fackeContext = Create.MockedDbContextFor<VideoApplicationDbContext>();
        }

        public class Comparer : IEqualityComparer<Genre>
        {
            public bool Equals(Genre x, Genre y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(null, y)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Name == y.Name;
            }

            public int GetHashCode([DisallowNull] Genre obj)
            {
                return HashCode.Combine(obj.Id, obj.Name);
            }
        }


        [Fact]
        public void GenreRepository_IsGenreRepository()
        {
            var genreRepo = new GenreRepository(_fackeContext);
            Assert.IsAssignableFrom<IGenreRepository>(genreRepo);
        }

        [Fact]
        public void GenreRepository_WithNullDbContext_ThrowInValidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new GenreRepository(null));
        }

        [Fact]
        public void GenreRepository_WithNullDbContext_ThrowExceptionMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() => new GenreRepository(null));
            Assert.Equal("", exception.Message);
        }

        [Fact]
        public void FindAll_GetAllGenresEntityInDbContext_AsListOfGenres()
        {
            var list = new List<GenreEntity>
            {
                new GenreEntity{Id=1,Name="test1"},
                new GenreEntity{Id=2,Name="test2"}
            };
            var repo = new GenreRepository(_fackeContext);
            //Integration
            _fackeContext.Set<GenreEntity>().AddRange(list);
            _fackeContext.SaveChanges();

            var expectedList = list.Select(gl => new Genre { Id = gl.Id, Name = gl.Name}).ToList();

            //Act
            var actualList = repo.GetAll();
            //Assert
            Assert.Equal(expectedList, actualList, new Comparer());
        }
    }
}
