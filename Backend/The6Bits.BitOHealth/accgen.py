from faker import Faker

fake = Faker()
file1 = open("MyFile.txt","a")

for i in range(0,10011):
  boof = "create "+fake.user_name()+" secuninodante@gmail.com Password!1 Dante Secundino 0 1\n"
  print(boof)
  file1.write(boof)