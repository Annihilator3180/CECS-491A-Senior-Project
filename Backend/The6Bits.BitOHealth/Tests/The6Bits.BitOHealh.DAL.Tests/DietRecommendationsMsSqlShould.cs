using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class DietRecommendationsMsSqlShould : TestsBase
    {
        private Contract.IRepositoryDietRecommendations _DietRecommendationsMsSqlDao;
        private DietRecommendationsService _DietRecommendationsService;
        private IDBErrors _dbErrors;

        public DietRecommendationsMsSqlShould()
        {
            _dbErrors = new MsSqlDerrorService();
            _DietRecommendationsMsSqlDao = new DietRecommendationsMsSqlDao(conn);
            _DietRecommendationsService = new DietRecommendationsService(_DietRecommendationsMsSqlDao, _dbErrors);
        }
        [Fact]
        public void SaveDietResponsesTest()
        {
            string expected = "1";
            DietR diet = new DietR("fruit", "low-fat", "peanut-free", 5, "dessert", 500, "American", "Almonds", "Lunch");
            string username = "Emily";
            string acctual = _DietRecommendationsMsSqlDao.SaveDietResponses(diet, username).Result;
            Assert.Equal(expected, acctual);
        }
        [Fact]
        public void AddToFavoriteTest()
        {
            string expected = "1";
            FavoriteRecipe fav = new FavoriteRecipe("id123456");
            string username = "Emily";
            string acctual = _DietRecommendationsMsSqlDao.AddToFavorite(fav, username).Result;
            Assert.Equal(expected, acctual);
        }
        [Fact]
        public void DeleteFavorite()
        {
            string expected = "1";
            FavoriteRecipe fav = new FavoriteRecipe("id123456");
            string username = "Emily";
            string added = _DietRecommendationsMsSqlDao.AddToFavorite(fav, username).Result;
            string acctual = _DietRecommendationsMsSqlDao.DeleteFavorite("id123456").Result;
            Assert.Equal(expected, acctual);
        }

        [Fact]
        public void GetFavorites()
        {
            List<string> expected = new List<string>
            { "id1", "id2", "id3" };
            string username = "EmilyS";
            FavoriteRecipe fav1 = new FavoriteRecipe("id1");
            FavoriteRecipe fav2 = new FavoriteRecipe("id2");
            FavoriteRecipe fav3 = new FavoriteRecipe("id3");
            string id1 = _DietRecommendationsMsSqlDao.AddToFavorite(fav1, username).Result;
            string id2 = _DietRecommendationsMsSqlDao.AddToFavorite(fav2, username).Result;
            string id3 = _DietRecommendationsMsSqlDao.AddToFavorite(fav3, username).Result;
            List<string> acctual = _DietRecommendationsMsSqlDao.GetFavorites(username).Result;
            Assert.Equal(expected, acctual);
            string del1 = _DietRecommendationsMsSqlDao.DeleteFavorite("id1").Result;
            string del2 = _DietRecommendationsMsSqlDao.DeleteFavorite("id2").Result;
            string del3 = _DietRecommendationsMsSqlDao.DeleteFavorite("id3").Result;


        }
        //negative testing
        [Fact]
        public void SaveDietResponsesTestNegative()
        {
            string expected = "1";
            DietR diet = new DietR("fruit", "low-fat", "peanut-free", 5, "dessert", 500, "American", "Almonds", "Lunch");
            string username = "EmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmily";
            string acctual = _DietRecommendationsMsSqlDao.SaveDietResponses(diet, username).Result;
            Assert.Equal(expected, acctual);
        }
        [Fact]
        public void AddToFavoriteTestNegative()
        {
            string expected = "1";
            FavoriteRecipe fav = new FavoriteRecipe("id12");
            string username = "EmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmilyEmily";
            string acctual = _DietRecommendationsMsSqlDao.AddToFavorite(fav, username).Result;
            Assert.Equal(expected, acctual);
        }

        [Fact]
        public void DeleteFavoriteNegative()
        {
            //create it first 
            string expected = "1";
            string acctual = _DietRecommendationsMsSqlDao.DeleteFavorite("id123456").Result;
            Assert.Equal(expected, acctual);
            //await
        }

        [Fact]
        public void GetFavoritesNegative()
        {
            List<string> expected = new List<string>
            { "id1", "id2", "id3" };
            string username = "EmilyS";
            FavoriteRecipe fav1 = new FavoriteRecipe("id1");
            FavoriteRecipe fav2 = new FavoriteRecipe("id2");
            // FavoriteRecipe fav3 = new FavoriteRecipe("id3");
            string id1 = _DietRecommendationsMsSqlDao.AddToFavorite(fav1, username).Result;
            string id2 = _DietRecommendationsMsSqlDao.AddToFavorite(fav2, username).Result;
            // string id3 = _DietRecommendationsMsSqlDao.AddToFavorite(fav3, username).Result;
            List<string> acctual = _DietRecommendationsMsSqlDao.GetFavorites(username).Result;
            Assert.Equal(expected, acctual);
        }


    }
}
