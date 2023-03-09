namespace CodeFlix.Catalog.UnitTest.Application.UpdateCategory
{
    public class UpdateCategoryTestDataGenerator
    {
        public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
        {
            var fixture = new UpdateCategoryTestFixture();
            for (int indice = 0; indice < times; indice++)
            {
                var exampleCategory = fixture.GetValidCategory();
                yield return new object[] { 
                    fixture.GetInput(exampleCategory.Id),
                    exampleCategory
                };
            }
        }
    }
}
