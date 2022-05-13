using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            string acctual = _DietRecommendationsMsSqlDao.SaveDietResponses(diet, username);
            Assert.Equal(expected, acctual);
        }
        [Fact]
        public void AddToFavoriteTest()
        {
            string expected = JsonSerializer.Serialize(new { success = true, message = "Added meal to meal list" });
            FavoriteRecipe fav = new FavoriteRecipe("id123456");
            string username = "Emilyy";
            string acctual = _DietRecommendationsMsSqlDao.AddToFavorite(fav, username);
            Assert.Equal(expected, acctual);
        }
        [Fact]
        public void DeleteFavorite()
        {
            string expected = JsonSerializer.Serialize(new { success = true, message = "Deleted meal from meal list" });
            FavoriteRecipe fav = new FavoriteRecipe("id123456");
            string username = "Emily";
            string added = _DietRecommendationsMsSqlDao.AddToFavorite(fav, username);
            string acctual = _DietRecommendationsMsSqlDao.DeleteFavorite(fav);
            Assert.Equal(expected, acctual);
        }

        [Fact]
        public void GetFavorites()
        {
            List<string> expected = new List<string>
            { "id1", "id2", "id3" };
            string username = "Emilyz";
            FavoriteRecipe fav1 = new FavoriteRecipe("id1");
            FavoriteRecipe fav2 = new FavoriteRecipe("id2");
            FavoriteRecipe fav3 = new FavoriteRecipe("id3");
            string id1 = _DietRecommendationsMsSqlDao.AddToFavorite(fav1, username);
            string id2 = _DietRecommendationsMsSqlDao.AddToFavorite(fav2, username);
            string id3 = _DietRecommendationsMsSqlDao.AddToFavorite(fav3, username);
            List<string> acctual = _DietRecommendationsMsSqlDao.GetFavorites(username);
            Assert.Equal(expected, acctual);
            string del1 = _DietRecommendationsMsSqlDao.DeleteFavorite(fav1);
            string del2 = _DietRecommendationsMsSqlDao.DeleteFavorite(fav2);
            string del3 = _DietRecommendationsMsSqlDao.DeleteFavorite(fav3);


        }
        //negative testing
        [Fact]
        public void SaveDietResponsesTestNegative()
        {
            string expected = "1";
            DietR diet = new DietR("fruit", "low-fat", "peanut-free", 5, "dessert", 500, "American", "Almonds", "Lunch");
            string username = "EmilyEmilyEmilyEmily";
            string acctual = _DietRecommendationsMsSqlDao.SaveDietResponses(diet, username);
            Assert.Equal(expected, acctual);
        }
        [Fact]
        public void AddToFavoriteTestNegative()
        {
            string expected = JsonSerializer.Serialize(new { success = false, message = "You reached today's limit. Come back tommorow!" });
            FavoriteRecipe fav = new FavoriteRecipe("id12");
            FavoriteRecipe fav2 = new FavoriteRecipe("id12");
            FavoriteRecipe fav3 = new FavoriteRecipe("id12");
            FavoriteRecipe fav4 = new FavoriteRecipe("id12");
            FavoriteRecipe fav5 = new FavoriteRecipe("id12");
            FavoriteRecipe fav6 = new FavoriteRecipe("id12");
            FavoriteRecipe fav7 = new FavoriteRecipe("id12");
            string username = "l";
            _DietRecommendationsMsSqlDao.AddToFavorite(fav, username);
            _DietRecommendationsMsSqlDao.AddToFavorite(fav2, username);
            _DietRecommendationsMsSqlDao.AddToFavorite(fav3, username);
            _DietRecommendationsMsSqlDao.AddToFavorite(fav4, username);
            _DietRecommendationsMsSqlDao.AddToFavorite(fav5, username);
            _DietRecommendationsMsSqlDao.AddToFavorite(fav6, username);
            string acctual = _DietRecommendationsMsSqlDao.AddToFavorite(fav7, username);


            Assert.Equal(expected, acctual);
        }

        [Fact]
        public void DeleteFavoriteNegative()
        {
            string expected = JsonSerializer.Serialize(new { success = false, message = "Failed to delete meal from meal list" });
            FavoriteRecipe fav = new FavoriteRecipe("id12345556");
            string username = "Emily";
            string acctual = _DietRecommendationsMsSqlDao.DeleteFavorite(fav);
            Assert.Equal(expected, acctual);
        }

        [Fact]
        public void GetFavoritesNegative()
        {
            List<string> expected = new List<string>
            { "id1", "id2"};
            string username = "EmilyR";
            FavoriteRecipe fav1 = new FavoriteRecipe("id1");
            FavoriteRecipe fav2 = new FavoriteRecipe("id2");
            // FavoriteRecipe fav3 = new FavoriteRecipe("id3");
            string id1 = _DietRecommendationsMsSqlDao.AddToFavorite(fav1, username);
            string id2 = _DietRecommendationsMsSqlDao.AddToFavorite(fav2, username);
            // string id3 = _DietRecommendationsMsSqlDao.AddToFavorite(fav3, username).Result;
            List<string> acctual = _DietRecommendationsMsSqlDao.GetFavorites(username);
            _DietRecommendationsMsSqlDao.DeleteFavorite(fav1);
            _DietRecommendationsMsSqlDao.DeleteFavorite(fav2);
            Assert.Equal(expected, acctual);
        }


    }
}
