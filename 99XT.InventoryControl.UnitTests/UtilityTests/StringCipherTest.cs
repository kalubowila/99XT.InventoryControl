using _99XT.InventoryControl.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace _99XT.InventoryControl.UnitTests.UtilityTests
{
    public class StringCipherTest
    {
        [Fact]
        public void IsEncrypted_String_ReturnsTrue()
        {
            //Arrange
            string stringToBeEncrypted = "TestString@123";

            //Act
            string encryptedString = StringCipher.Encrypt(stringToBeEncrypted);

            //Assert
            Assert.NotEqual(encryptedString, stringToBeEncrypted);
        }

        [Fact]
        public void IsDecrypted_String_ReturnsTrue()
        {
            //Arrange
            string stringToBeEncrypted = "TestString@123";
            
            //Act
            string encryptedString = StringCipher.Encrypt(stringToBeEncrypted);
            string decryptedString = StringCipher.Decrypt(encryptedString);

            //Assert
            Assert.Equal(stringToBeEncrypted, decryptedString);
        }
    }
}
