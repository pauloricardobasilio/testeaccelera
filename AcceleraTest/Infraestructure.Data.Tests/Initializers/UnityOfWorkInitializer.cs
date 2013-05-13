using System;
using System.Data.Entity;
using Domain;

namespace Infraestructure.Data.Tests.Initializers
{
    public class UnityOfWorkInitializer : DropCreateDatabaseAlways<UnityOfWork>
    {
        protected override void Seed(UnityOfWork unityOfWork)
        {
            var fakeColaborator = new Colaborator
                                      {
                                          Name = "Fake Colaborator",
                                          DateOfBirth = new DateTime(1990, 01, 01),
                                          Registry = "11",
                                          PhoneNumber = "(69) 9999-9999",
                                          Address = "Fake Street, 11",
                                          Estate = "FE",
                                          City = "Fake City"
                                      };

            var fakeColaborator2 = new Colaborator
                                        {
                                            Name = "Fake Colaborator 2",
                                            DateOfBirth = new DateTime(1991, 01, 01),
                                            Registry = "22",
                                            PhoneNumber = "(69) 9999-9999",
                                            Address = "Fake Street, 22",
                                            Estate = "FE",
                                            City = "Fake City"
                                        };

            var fakeColaborator3 = new Colaborator
                                      {
                                          Name = "Fake Colaborator 3",
                                          DateOfBirth = new DateTime(1990, 01, 01),
                                          Registry = "33",
                                          PhoneNumber = "(69) 9999-9999",
                                          Address = "Fake Street, 33",
                                          Estate = "FE",
                                          City = "Fake City"
                                      };

            var fakeColaborator4 = new Colaborator
                                      {
                                          Name = "Fake Colaborator 4",
                                          DateOfBirth = new DateTime(1990, 01, 01),
                                          Registry = "44",
                                          PhoneNumber = "(69) 9999-9999",
                                          Address = "Fake Street, 44",
                                          Estate = "FE",
                                          City = "Fake City"
                                      };

            unityOfWork.Colaborators.Add(fakeColaborator);
            unityOfWork.Colaborators.Add(fakeColaborator2);
            unityOfWork.Colaborators.Add(fakeColaborator3);
            unityOfWork.Colaborators.Add(fakeColaborator4);
        }
    }
}
