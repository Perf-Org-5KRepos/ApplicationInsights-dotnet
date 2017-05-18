﻿namespace Microsoft.ApplicationInsights.WindowsServer.Implementation
{
    using System;
    using System.Globalization;
    using Microsoft.ApplicationInsights.WindowsServer.Azure;
    using Microsoft.ApplicationInsights.WindowsServer.Azure.Emulation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Assert = Xunit.Assert;

    /// <summary>    
    /// Tests ServiceRuntime by supplying it with a custom version of Microsoft.WindowsAzure.ServiceRuntime and testing
    /// if it invokes the right methods and reads values as expected.
    /// The supplied Microsoft.WindowsAzure.ServiceRuntime is a proxy to the real one, and this simulates
    /// as if the tests are running inside a cloud service environment.
    /// Read ServiceRuntimeHelper.cs to see how the actual calls are intercepted.
    /// </summary>
    [TestClass]    
    public class ServiceRuntimeTests
    {
        [TestMethod]
        [Ignore]
        public void RoleEnvironmentIsAvailableReturnsFalseIfServiceRuntimeDoesntExit()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);
            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            roleEnvironment.TargetType = null;
            Assert.NotNull(roleEnvironment);
            Assert.False(roleEnvironment.IsAvailable);
        }

        [TestMethod]
        [Ignore]
        public void ServiceRuntimeProducesARoleEnvironmentObject()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);

            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            Assert.NotNull(roleEnvironment);
        } 

        [TestMethod]
        [Ignore]
        public void RoleEnvironmentReturnsCorrectAvailabilityState()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);
            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            Assert.NotNull(roleEnvironment);

            Assert.Equal(ServiceRuntimeHelper.IsAvailable, roleEnvironment.IsAvailable);

            ServiceRuntimeHelper.IsAvailable = !ServiceRuntimeHelper.IsAvailable;

            Assert.Equal(ServiceRuntimeHelper.IsAvailable, roleEnvironment.IsAvailable);
        }

        [TestMethod]
        [Ignore]
        public void RoleEnvironmentReturnsCorrectDeploymentId()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);
            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            Assert.NotNull(roleEnvironment);

            Assert.Equal(ServiceRuntimeHelper.DeploymentId, roleEnvironment.DeploymentId);

            ServiceRuntimeHelper.DeploymentId = Guid.NewGuid().ToString("N");

            Assert.Equal(ServiceRuntimeHelper.DeploymentId, roleEnvironment.DeploymentId);
        }

        [TestMethod]
        [Ignore]
        public void RoleEnvironmentReturnsTheCurrentRoleInstanceWhichIsNotNull()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);
            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            Assert.NotNull(roleEnvironment);

            RoleInstance roleInstance = roleEnvironment.CurrentRoleInstance;
            Assert.NotNull(roleInstance);
        }

        [TestMethod]
        [Ignore]
        public void RoleInstanceReturnAnInstanceIdThatMatchesAnAzureInstanceId()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);
            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            Assert.NotNull(roleEnvironment);

            RoleInstance roleInstance = roleEnvironment.CurrentRoleInstance;
            Assert.NotNull(roleInstance);

            string expectedId = string.Format(
                                    CultureInfo.InvariantCulture,
                                    TestRoleInstance.IdFormat,
                                    ServiceRuntimeHelper.RoleName,
                                    ServiceRuntimeHelper.RoleInstanceOrdinal);
            Assert.Equal(expectedId, roleInstance.Id);

            ServiceRuntimeHelper.RoleName = "MyTestRole_" + Guid.NewGuid().ToString("N");
            ServiceRuntimeHelper.RoleInstanceOrdinal = new Random().Next(0, 10);

            roleInstance = roleEnvironment.CurrentRoleInstance;
            Assert.NotNull(roleInstance);

            expectedId = string.Format(
                                    CultureInfo.InvariantCulture,
                                    TestRoleInstance.IdFormat,
                                    ServiceRuntimeHelper.RoleName,
                                    ServiceRuntimeHelper.RoleInstanceOrdinal);
            Assert.Equal(expectedId, roleInstance.Id);
        }

        [TestMethod]
        [Ignore]
        public void RoleInstanceReturnsARollWhichIsNotNull()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);
            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            Assert.NotNull(roleEnvironment);

            RoleInstance roleInstance = roleEnvironment.CurrentRoleInstance;
            Assert.NotNull(roleInstance);

            Role role = roleInstance.Role;
            Assert.NotNull(role);
        }

        [TestMethod]
        [Ignore]
        public void RoleReturnsCorrectName()
        {
            ServiceRuntime serviceRuntime = new ServiceRuntime(typeof(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment).Assembly);
            RoleEnvironment roleEnvironment = serviceRuntime.GetRoleEnvironment();
            Assert.NotNull(roleEnvironment);

            RoleInstance roleInstance = roleEnvironment.CurrentRoleInstance;
            Assert.NotNull(roleInstance);

            Role role = roleInstance.Role;
            Assert.NotNull(role);

            Assert.Equal(ServiceRuntimeHelper.RoleName, role.Name);

            ServiceRuntimeHelper.RoleName = "MyTestRole_" + Guid.NewGuid().ToString("N");

            roleInstance = roleEnvironment.CurrentRoleInstance;
            Assert.NotNull(roleInstance);

            role = roleInstance.Role;
            Assert.NotNull(role);

            Assert.Equal(ServiceRuntimeHelper.RoleName, role.Name);
        }
    }
}
