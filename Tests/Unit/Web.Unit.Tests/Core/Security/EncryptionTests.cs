﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityEssentials.Core;

namespace SecurityEssentials.Unit.Tests.Core.Security
{
    /// <summary>
    /// Summary description for Encryption2
    /// </summary>
    [TestClass]
    public class Encryption
    {
        

        private SecurityEssentials.Core.Encryption _testEncrypt;
        private SecurityEssentials.Core.Encryption _testDecrypt;
        private string _salt = "abccdefghijklmnop123456789";
        private string _password = "qrstuvwxzy1234567890";
        private int _iterationCount = 68;

        public Encryption()
        {

            _testEncrypt = new SecurityEssentials.Core.Encryption();
            _testDecrypt = new SecurityEssentials.Core.Encryption();

        }
        
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _testEncrypt.Dispose();
            _testDecrypt.Dispose();
        }
        
        [TestMethod]
        public void EnDecrypt_BasicMessage_Succeeds()
        {

            string input = "John Was here John was here again";
            string output;
            string encrypted;
            Assert.AreEqual(true, _testEncrypt.Encrypt(_password, _salt, _iterationCount, input, out encrypted), "Encryption was not successful");
            Assert.AreNotEqual(input, encrypted);
            Assert.AreEqual(true, _testDecrypt.Decrypt(_password, _salt, _iterationCount, encrypted, out output), "Decryption was not successful");
            Assert.AreEqual(input, output);

        }

        [TestMethod]
        public void EnDecrypt_ComplexMessage_Succeeds()
        {

            string input = "john was here 1234567890!£$%^&**()-_=+[]{}';,./#:@~?><  \\|`¬";
            string output;
            string encrypted;
            Assert.AreEqual(true, _testEncrypt.Encrypt(_password, _salt, _iterationCount, input, out encrypted), "Encryption was not successful");
            Assert.AreNotEqual(input, encrypted);
            Assert.AreEqual(true, _testDecrypt.Decrypt(_password, _salt, _iterationCount, encrypted, out output), "Decryption was not successful");
            Assert.AreEqual(input, output);

        }

        [TestMethod]
        public void EnDecrypt_BlankString_Succeeds()
        {

            string input = "";
            string output;
            string encrypted;
            Assert.AreEqual(true, _testEncrypt.Encrypt(_password, _salt, _iterationCount, input, out encrypted));
            Assert.AreNotEqual(input, encrypted);
            Assert.AreEqual(true, _testDecrypt.Decrypt(_password, _salt, _iterationCount, encrypted, out output));
            Assert.AreEqual(input, output);

        }

        [TestMethod]
        public void Encrypt_Null_DecryptsToZeroLengthString()
        {

            string input = null;
            string encrypted = null;
            string output;
            Assert.AreEqual(true, _testEncrypt.Encrypt(_password, _salt, _iterationCount, input, out encrypted));
            Assert.AreEqual(true, _testDecrypt.Decrypt(_password, _salt, _iterationCount, encrypted, out output));
            Assert.AreNotEqual(input, output);
            Assert.AreEqual("", output);

        }

        [TestMethod]
        public void EnDecrypt_Message_ChangeIterations_Fails()
        {

            string input = "The quick brown fox jumped over the lazy dog";
            string output;
            string encrypted;
            Assert.AreEqual(true, _testEncrypt.Encrypt(_password, _salt, _iterationCount, input, out encrypted));
            Assert.AreNotEqual(input, encrypted);
            Assert.AreNotEqual(true, _testDecrypt.Decrypt(_password, _salt, _iterationCount + 1, encrypted, out output));
            Assert.AreNotEqual(input, output);

        }


        [TestMethod]
        public void EnDecrypt_Message_ChangeSalt_Fails()
        {

            string input = "The quick brown fox jumped over the lazy dog";
            string output;
            string encrypted;
            Assert.AreEqual(true, _testEncrypt.Encrypt(_password, _salt, _iterationCount, input, out encrypted));
            Assert.AreNotEqual(input, encrypted);
            Assert.AreNotEqual(true, _testDecrypt.Decrypt(_password, string.Format("{0}1", _salt), _iterationCount, encrypted, out output));
            Assert.AreNotEqual(input, output);

        }

        [TestMethod]
        public void EnDecrypt_Message_ChangePassword_Fails()
        {

            string input = "The quick brown fox jumped over the lazy dog";
            string output;
            string encrypted;
            Assert.AreEqual(true, _testEncrypt.Encrypt(_password, _salt, _iterationCount, input, out encrypted));
            Assert.AreNotEqual(input, encrypted);
            Assert.AreNotEqual(true, _testDecrypt.Decrypt(string.Format("{0}1", _password), _salt, _iterationCount, encrypted, out output));
            Assert.AreNotEqual(input, output);

        }
        
    }
}
