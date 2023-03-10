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

        public static IEnumerable<object[]> GetInvalidInputs(int times = 15)
        {
            var fixture = new UpdateCategoryTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 5;

            for (int index = 0; index < times; index++)
            {
                var exampleCategory = fixture.GetValidCategory();

                switch (index % totalInvalidCases)
                {
                    case 0:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputShortName(exampleCategory.Id),
                            exampleCategory,
                            "Name should be at least 3 characters long"
                        });
                        break;
                    case 1:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputTooLongName(exampleCategory.Id),
                            exampleCategory,
                            "Name should be less or equal 255 characters long"
                        });
                        break;
                    case 2:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputNameNull(exampleCategory.Id),
                            exampleCategory,
                            "Name should not be null or empty"
                        });
                        break;
                    case 3:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputNameEmpty(exampleCategory.Id),
                            exampleCategory,
                            "Name should not be null or empty"
                        });
                        break;
                    case 4:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputTooLongDescription(exampleCategory.Id),
                            exampleCategory,
                            "Description should be less or equal 10000 characters long"
                        });
                        break;
                    default:
                        break;
                }
            }
            return invalidInputsList;
        }
    }
}
