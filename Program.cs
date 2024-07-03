using ConsoleProjectCodeAcademy.Exceptions;
using ConsoleProjectCodeAcademy.Models;
using ConsoleProjectCodeAcademy.Services;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace ConsoleProjectCodeAcademy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User activeUser = new ("","","");
            UserService userService = new UserService();
            CategoryService categoryService = new CategoryService();
          
            MedicineService medicineService = new MedicineService();
            bool exit = false;

            while (!exit)
            {
            restart:
                Console.WriteLine("Menu: ");
                Console.WriteLine("1.User Registration");
                Console.WriteLine("2.User Login");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();
              
                switch (choice)
                {
                    case "1"://registration
                    registration:
                        Console.WriteLine("Please, enter fullname: ");
                        string fullname = Console.ReadLine();
                        Console.WriteLine("Please, enter email: ");
                        string email = Console.ReadLine();
                        Console.WriteLine("Please, enter password: ");
                        string password = Console.ReadLine();

                        User user = new User(fullname, email, password);
                        userService.AddUser(user);
                      
                        goto restart;
                
                        
                    case "2":
                        Console.WriteLine("Do you have an account");
                        string answer = Console.ReadLine();
                        if(!(answer == "Yes" || answer == "YES"))
                        {
                        Console.WriteLine("Please, register...");
                        goto registration;
                        }
                       
                        Console.WriteLine("Enter email: ");
                        string loginEmail = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        string loginPassword = Console.ReadLine();
                        try
                        {
                        activeUser = userService.Login(loginEmail, loginPassword);
                        Console.WriteLine($"{activeUser.Fullname} welcome");

                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto restart;
                        }

                        break;

                    case "0":
                        exit = true;
                        return;

                }
                bool userExit = false;
                while (!userExit)
                {
                    Console.WriteLine("Please, select the choice: ");
                    Console.WriteLine("6.Create new category");
                    Console.WriteLine("1.Create new medicine");
                    Console.WriteLine("2.Show all medicines");
                    Console.WriteLine("3.Show medicines for their categories");
                    Console.WriteLine("4.Show medicines for their id");
                    Console.WriteLine("5.Show medicines for their name");
                    Console.WriteLine("7.Remove medicine");
                    Console.WriteLine("8.Update medicine");
                    Console.WriteLine("9.Exit program");
                    Console.WriteLine("Enter your choice: ");
                    string command = Console.ReadLine();

                    switch (command)
                    {


                        case "1":
                            try
                            {
                                Console.WriteLine("Enter the information of new medicine: ");
                                Console.WriteLine("The name of medicine: ");
                                string medicineName = Console.ReadLine();
                                Console.WriteLine("Price of medicine: ");
                                decimal price = int.Parse(Console.ReadLine());

                                foreach (var category in DB.Categories)
                                {
                                    Console.WriteLine(category);
                                }
                                Console.WriteLine("The id of category: ");
                                int categoryId = int.Parse(Console.ReadLine());
                                Medicine newMedicine1 = new Medicine(medicineName, price, activeUser.Id, categoryId);
                                medicineService.CreateMedicine(newMedicine1);
                                Console.WriteLine("New medicine added succesfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;
                        case "2":
                            try
                            {
                               
                                Medicine[] allMedicines = medicineService.GetAllMedicines();
                                Console.WriteLine("\nAll medicines: ");
                                if (allMedicines.Length == 0)
                                {
                                    Console.WriteLine("Medicine not found");
                                }
                                else { 
                                foreach (var Medicine in allMedicines)
                                {
                                    Console.WriteLine(Medicine.ToString());
                                }
                            }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;
                        case "3"://category  SORUS duzdurmu
                            try
                            {
                                Console.WriteLine("\nEnter category id: ");
                                int categorySearchId = int.Parse(Console.ReadLine());
                                medicineService.GetMedicineByCategory(categorySearchId);
                                Console.WriteLine($"Category id {categorySearchId} medicines: ");
                                foreach (var Medicine in DB.Medicines)
                                {
                                    Console.WriteLine(Medicine);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }

                            break;
                        case "4"://id
                            try
                            {
                                Console.WriteLine("\nEnter the id: ");
                                int searchId = int.Parse(Console.ReadLine());
                                medicineService.GetMedicineById(searchId);
                                Console.WriteLine($"{searchId} medicines: ");
                                foreach (var Medicine in DB.Medicines)
                                {
                                    Console.WriteLine(Medicine);
                                }

                            }
                            catch (NotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }

                            break;
                        case "5"://name
                            try
                            {
                                Console.WriteLine("\n Enter the name: ");
                                string searchName = Console.ReadLine();
                                medicineService.GetMedicineByName(searchName);
                                Console.WriteLine($"{searchName} medicines: ");
                                foreach (var Medicine in DB.Medicines)
                                {
                                    Console.WriteLine(Medicine);
                                }
                            }
                            catch (NotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;
                        case "6"://create category
                            try
                            {
                                Console.WriteLine("\nEnter the name of category: ");
                                string categoryName = Console.ReadLine();
                                Category newCategory1 = new Category(categoryName);
                                categoryService.CreateCategory(newCategory1);
                                Console.WriteLine("Category succesfully created!");
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;
                        case "7":
                            try
                            {
                                Console.WriteLine("\nEnter medicine ID to remove: ");
                                int medicineIdRemove = int.Parse(Console.ReadLine());
                                medicineService.RemoveMedicine(medicineIdRemove);
                                Console.WriteLine($"{medicineIdRemove} removed");
                            }
                            catch (NotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }

                            break;
                        case "8"://update
                            try
                            {
                                Console.WriteLine("\nEnter medicine ID to update: ");
                                int medicineUpdate = int.Parse(Console.ReadLine());

                                Console.WriteLine("Enter updated medicine names: ");
                                Console.WriteLine("Name: ");
                                string updateMedicineName = Console.ReadLine();
                                Console.WriteLine("Price: ");
                                decimal updatedMedicinePrice = decimal.Parse(Console.ReadLine());
                                Console.WriteLine("Category ID: ");
                                int updatedCategoryId = int.Parse(Console.ReadLine());
                                Medicine updatedMedicine = new Medicine("Parestamol", 10, activeUser.Id, 2);


                                medicineService.UpdateMedicine(medicineUpdate,updatedMedicine);
                                Console.WriteLine($"{updatedMedicine} updated");
                            }
                            catch (NotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;
                        case "9":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("FALSE CHOICE...Please, try again...");
                            break;


                    }
                }
            }
            






































































            //Medicine medicine = new Medicine
            //{
            //    Name = "Parestamol",
            //    Price = 10,
            //    UserId = 1,
            //    CategoryId = 2,
            //    Id = 3,
            //};
            //UserService userService = new UserService();
            // MedicineService medicineService = new MedicineService();
            //User user1 = new User("Aysun Eminli","aem","123");
            //userService.AddUser(user1);


            // Console.WriteLine("Please, enter email and password: ");
            // Console.WriteLine("Email: ");
            // string email = Console.ReadLine();
            // Console.WriteLine("Password: ");
            // string password = Console.ReadLine();

            // try
            // {
            //     User loggedUser = userService.Login(email, password);
            //     Console.WriteLine("Succesfully accessed...");
            //     bool exit = false;
            //     while (!exit)
            //     {
            //         Console.WriteLine("Please, select the choice: ");
            //         Console.WriteLine("1.Add new medicine");
            //         Console.WriteLine("2.Show all medicines");
            //         Console.WriteLine("3.Show medicines for their categories");
            //         //id
            //         //name
            //         //create category
            //         Console.WriteLine("4.Remove medicine");
            //         Console.WriteLine("5.Update medicine");
            //         Console.WriteLine("6.Exit program");
            //         Console.WriteLine("Enter your choice: ");
            //         string choice = Console.ReadLine();

            //         switch (choice)
            //         {
            //             case "1":
            //                 Console.WriteLine("Enter the information of new medicine: ");
            //                 Console.WriteLine("The name of medicine: ");
            //                 string medicineName = Console.ReadLine();
            //                 Console.WriteLine("Price of medicine: ");
            //                 decimal price = int.Parse(Console.ReadLine());
            //                 Console.WriteLine("The id of category: ");
            //                 int categoryId = int.Parse(Console.ReadLine());
            //                 Medicine newMedicine1 = new Medicine
            //                 {
            //                     Name = medicineName,
            //                     Price = price,
            //                     CategoryId = categoryId,
            //                     UserId = user1.Id,
            //                     CreatedTime = DateTime.Now,

            //                 };
            //                 medicineService.CreateMedicine(newMedicine1);
            //                 Console.WriteLine("New medicine added succesfully.");
            //                 break;
            //             case "2":
            //                 Medicine[] allMedicines = medicineService.GetAllMedicines();
            //                 Console.WriteLine("\nAll medicines: ");
            //                 foreach (var Medicine in allMedicines)
            //                 {
            //                     Console.WriteLine($"ID: {Medicine.Id}, Name: {Medicine.Name}, Price: {Medicine.Price}, Category ID: {Medicine.CategoryId})");
            //                 }
            //                 break;
            //             case "3":
            //                 Console.WriteLine("\nEnter category id: ");
            //                 int categorySearchId = int.Parse(Console.ReadLine());
            //                 Medicine[] medicines = medicineService.GetAllMedicines();
            //                 Console.WriteLine($"category id {categorySearchId} medicines: ");
            //                 foreach (var Medicine in medicines)
            //                 {
            //                     Console.WriteLine(Medicine);
            //                 }

            //                 break;
            //             case "4":
            //                 Console.WriteLine("\nEnter medicine ID to remove: ");
            //                 int medicineIdRemove = int.Parse(Console.ReadLine());
            //                 medicineService.RemoveMedicine(medicineIdRemove);
            //                 Console.WriteLine($"{medicineIdRemove} removed");
            //                 break;
            //             case "5"://update
            //                 Console.WriteLine("\nEnter medicine ID to update: ");
            //                 int medicineUpdate = int.Parse(Console.ReadLine());

            //                 Console.WriteLine("Enter updated medicine names: ");
            //                 Console.WriteLine("Name: ");
            //                 string updateMedicineName = Console.ReadLine();
            //                 Console.WriteLine("Price: ");
            //                 decimal updatedMedicinePrice = decimal.Parse(Console.ReadLine());
            //                 Console.WriteLine("Category ID: ");
            //                 int updatedCategoryId = int.Parse(Console.ReadLine());
            //                 Medicine updatedMedicine = new Medicine
            //                 {
            //                     Id = medicineUpdate,
            //                     Name = updateMedicineName,
            //                     Price = updatedMedicinePrice,
            //                     CategoryId = updatedCategoryId,
            //                     UserId = medicineUpdate,
            //                     CreatedTime = DateTime.Now,

            //                 };
            //                 //SORUS
            //                 medicineService.CreateMedicine(updatedMedicine);
            //                 Console.WriteLine($"{updatedMedicine} updated");
            //                 break;
            //             case "6":
            //                 exit = true;
            //                 break;
            //             default:
            //                 Console.WriteLine("FALSE CHOICE...Please select a valid choice");
            //                 break;
            //         }
            //     }

            // }
            // catch(NotFoundException ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            // catch(Exception ex) {
            //     Console.WriteLine("An error occured: " +ex.Message);
            // }

        }
    }
}
