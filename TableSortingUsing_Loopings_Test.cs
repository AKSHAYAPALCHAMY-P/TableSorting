namespace TableSortingUsing_Loopings_Test
{
    [TestClass]
    public sealed class TableSortingUsing_Loopings_Test
    {
        [TestMethod]
        public void TestSimpleHierarchy()
        {
            //Arrange
            var tables = new List<Table>
            {
            new Table("Invoice", new List<string> { "Payment" }),
            new Table("Payment", new List<string> { "Bank" }),
            new Table("Bank", new List<string>()) 
            };

            var expectedOrder = new List<string> { "Bank", "Payment", "Invoice" };

            // Act
            var result = TableSorter.SortTables(tables);

            // Assert
            CollectionAssert.AreEqual(expectedOrder, result);
        }


        [TestMethod]
        public void SortTables_ShouldThrowCycleDetectedError_WhenCycleExists()
        {
            //Arrange
            var tables = new List<Table>
            {
                new Table("A", new List<string> { "B" }),
                new Table("B", new List<string> { "C" }),
                new Table("C", new List<string> { "A" }) 
            };

            //Act
            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => TableSorter.SortTables(tables));

            //Assert
            Assert.AreEqual("Cycle Detected", exception.Message);
        }

    }
}
