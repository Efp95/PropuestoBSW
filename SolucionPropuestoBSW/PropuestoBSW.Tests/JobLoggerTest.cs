using System;
using NUnit.Framework;
using FluentAssertions;
using PropuestoBSW.Core;

namespace PropuestoBSW.Tests
{
    #region Comments
    // Question 2.2. Unit Tests
    #endregion

    [TestFixture]
    public class JobLoggerTest
    {

        [SetUp]
        public void Setup()
        {
            //
        }

        [TearDown]
        public void Teardown()
        {
            //
        }


        [Test]
        public void CallLoggerWithoutMessage_ReturnEmpty()
        {
            string result = String.Empty;
            result = NewJobLogger.LogMessage(result, null, false, false, false);

            result.Should().BeEmpty();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Invalid configuration")]
        public void CallLoggerWithoutDestiny_ReturnException()
        {
            string result = String.Empty;
            string error = "Fatal Error !!";
            result = NewJobLogger.LogMessage(error, MessageType.Message, false, false, false);

            result.Should().BeEmpty();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Error or Warning or Message must be specified")]
        public void CallLoggerWithoutMessageType_ReturnException()
        {
            string result = String.Empty;
            string error = "Fatal Error !!";
            result = NewJobLogger.LogMessage(error, null, true, true, true);
            
            result.Should().BeEmpty();
        }


        [Test]
        public void CallLoggerToConsole_ReturnSuccess()
        {
            string result = String.Empty;
            string error = "Fatal Error !!";
            result = NewJobLogger.LogMessage(error, MessageType.Warning, true, false, false);

            result.Should().Be("success");
        }

        [Test]
        public void CallLoggerToFile_ReturnSuccess()
        {
            string result = String.Empty;
            string error = "Fatal Error !!";
            result = NewJobLogger.LogMessage(error, MessageType.Warning, false, true, false);

            result.Should().Be("success");
        }

        [Test]
        public void CallLoggerToDatabase_ReturnSuccess()
        {
            string result = String.Empty;
            string error = "Fatal Error !!";
            result = NewJobLogger.LogMessage(error, MessageType.Warning, false, false, true);

            result.Should().Be("success");
        }

        [Test]
        public void CallLoggerToAll_ReturnSuccess()
        {
            string result = String.Empty;
            string error = "Fatal Error !!";
            result = NewJobLogger.LogMessage(error, MessageType.Warning, true, true, true);

            result.Should().Be("success");
        }

    }
}
