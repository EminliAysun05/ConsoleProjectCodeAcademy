

using ConsoleProjectCodeAcademy.Exceptions;
using ConsoleProjectCodeAcademy.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace ConsoleProjectCodeAcademy.Services
{
    public class MedicineService
    {

        public void CreateMedicine(Medicine Medicine)
        {
            foreach (var category in DB.Categories)
            {
                if (category.Id == Medicine.CategoryId)
                {
                    Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
                    DB.Medicines[DB.Medicines.Length - 1] = Medicine;
                    return;
                }
            }
            throw new NotFoundException($"Id not found...");


        }


        public Medicine[] GetAllMedicines()
        {
            return DB.Medicines;
        }
        //sorus exception lazimdimi
        public Medicine GetMedicineById(int id)
        {
            foreach (var Medicine in DB.Medicines)
            {
                if (Medicine.Id == id)
                {
                    return Medicine;
                }
            }
            throw new NotFoundException($"Medicine with ID {id} not found...");
        }
        public Medicine GetMedicineByName(string name)
        {
            foreach (var Medicine in DB.Medicines)
            {
                if (Medicine.Name == name)
                {
                    return Medicine;
                }

            }
            throw new NotFoundException($"Medicine with name {name} not found...");
        }
        //sorus2
        public void GetMedicineByCategory(int categoryId)
        {
            foreach (var Medicine in DB.Medicines)
            {
                if (Medicine.CategoryId == categoryId)
                {
                    Console.WriteLine(Medicine);
                }
            }
        }
        public void RemoveMedicine(int id)
        {

            for (int i = 0; i < DB.Medicines.Length; i++)
            {
                if (DB.Medicines[i].Id == id)
                {
                    for (int j = i; j < DB.Medicines.Length; j++)
                    {
                        DB.Medicines[j] = DB.Medicines[j + 1];
                    }
                    Array.Resize(ref DB.Medicines, DB.Medicines.Length - 1);
                    break;
                }

            }
            throw new NotFoundException("Medicine with ID {id} not found...");
        }
        public void UpdateMedicine(int id, Medicine newMedicine)
        {
            foreach (var medicine in DB.Medicines)
            {
                if(medicine.Id== id)
                {
                    medicine.Name = newMedicine.Name;
                    medicine.Price = newMedicine.Price;
                    medicine.CategoryId = newMedicine.CategoryId;

                    return;
                }

            }
            throw new NotFoundException("Id of medicien is not found...");
        }

    }
}
