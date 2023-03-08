﻿namespace CodeFlix.Catalog.UnitTest.Application.CreateCategory
{
    public class CreateCategoryTestDataGenerator
    {
        public static IEnumerable<object[]> GetInvalidInputs(int times = 18)
        {
            var fixture = new CreateCategoryTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 6;

            for (int index = 0; index < times; index++)
            {
                switch (index % totalInvalidCases)
                {
                    case 0:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputShortName(),
                            "Name should be at least 3 characters long"
                        });
                        break;
                    case 1:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputTooLongName(),
                            "Name should be less or equal 255 characters long"
                        });
                        break;
                    case 2:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputNameNull(),
                            "Name should not be null or empty"
                        });
                        break;
                    case 3:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputNameEmpty(),
                            "Name should not be null or empty"
                        });
                        break;
                    case 4:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputDescriptionNull(),
                            "Description should not be null"
                        });
                        break;
                    case 5:
                        invalidInputsList.Add(new object[] {
                            fixture.GetInvalidInputTooLongDescription(),
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
