using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private BankVault vault;
        private Item item;


        [SetUp]
        public void Setup()
        {
            vault = new BankVault();
            item = new Item("me", "1");
        }

        [Test]
        public void WhenCellDoesNotExist_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.AddItem("go nqma", item);
            });

            Assert.AreEqual(ex.Message, "Cell doesn't exists!");
        }

        [Test]
        public void WhenCellIsAlreadyTaken_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.AddItem("A2", item);
                vault.AddItem("A2", new Item("Gosho", "4"));
            });

            Assert.AreEqual(ex.Message, "Cell is already taken!");
        }

        [Test]
        public void WhenItemIsAlreadyInCell_ShouldThrowException()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() =>
            {
                vault.AddItem("A1", item);
                vault.AddItem("A2", item);
            });

            Assert.AreEqual(ex.Message, "Item is already in cell!");
        }

        [Test]
        public void WhenItemIsAdded_ShouldReturnCorrectMessage()
        {
            string result = vault.AddItem("A1", item);

            Assert.AreEqual(result, $"Item:{item.ItemId} saved successfully!");
        }
        [Test]
        public void WhenItemIsAdded_ShouldSetItemToCell()
        {
            string result = vault.AddItem("A1", item);

            Assert.AreEqual(item, vault.VaultCells["A1"]);
        }

        [Test]
        public void WhenRemoveCalledAndCellDoesNotExist_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.RemoveItem("go nqma", item);
            });

            Assert.AreEqual(ex.Message, "Cell doesn't exists!");
        }
        [Test]
        public void WhenRemoveCalledAndItemDoesNotExist_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.RemoveItem("A2", item);
            });

            Assert.AreEqual(ex.Message, $"Item in that cell doesn't exists!");
        }

        [Test]
        public void WhenItemIsRemoved_ShouldReturnCorrectMessage()
        {
            vault.AddItem("A1", item);
            string result = vault.RemoveItem("A1", item);

            Assert.AreEqual(result, $"Remove item:{item.ItemId} successfully!");
        }
        [Test]
        public void WhenItemIsRemoved_ShouldMakeTheCellNull()
        {
            vault.AddItem("A1", item);

            string result = vault.RemoveItem("A1", item);

            Assert.AreEqual(null, vault.VaultCells["A1"]);
        }
    }
}