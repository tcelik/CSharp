using System;

namespace LambdaExpression
{
    //Delege bildirimleri
    delegate void Proc1(string a);
    delegate int Proc2(int a, int b);
    delegate void Proc3();
    delegate bool IPredicate(int val);

    class MainClass {
        public static void Main(string[] args)
        {
            int [] src = { 4, 8, 3, 1, 18, 9, 21, 20, 5, 17, 1000003};
            int [] dest = new int[src.Length];

            //Öyle bir method yazınki neyi kopyaladığını bilmesin ama kopyalasın.
            //delege türe ne geçebilirim tabiki fonksiyon ismi yani adresi o da içeride çağırsa
            //src'tan deste asalsa demek oldu.
            //asal için ayrı, even, odd için ayrı method yazmamış oldun.
            int index = copyIf(src, dest, NumberUtil.isPrime);
            printArray(dest, index);

            index = copyIf(src, dest, NumberUtil.isEven);
            printArray(dest, index);

            index = copyIf(src, dest, NumberUtil.isOdd);
            printArray(dest, index);

            Console.WriteLine("**");
            index = copyIf(src, dest, val => NumberUtil.isPrime(val));
            printArray(dest, index);


            index = copyIf(src, dest, val => val % 2 == 0);
            printArray(dest, index);


            //lambda ile code yazmak da mümkün.
            Console.WriteLine("**");
            index = copyIf(src, dest, val =>
            {
                return val % 2 == 0;
            });

            printArray(dest, index);


        }

        //ismi copyIf oldu, parametre lazım görüyor musun(if bilgisi,delege tür), ne kopyaladığını bilmeyen method.
        public static int copyIf(int [] src, int [] dest, IPredicate pred)
        {
            int index = 0;

            foreach (int val in src)
                if (pred(val)) //asalsa, tekse, çiftse ben bilmiyorum kardeşim diyor.
                    dest[index++] = val;

            return index;
        }

        //ilk aklan gelen yöntemler, fonksiyonel olmayan :)
        public static void NormalFonksiyonelOlmayanYontem(int [] src, int [] dest)
        {

            int index = copyAsal(src, dest);
            printArray(dest, index);

            //çiftleri kopyalamak
            index = copyEven(src, dest);
            printArray(dest, index);

            //tekleri kopyalamak
            index = copyOdd(src, dest);
            printArray(dest, index);

            //çift olup, 3'e bölünebilenleri yapan bir method.
            index = copyEvenAndDivide3(src, dest);
            printArray(dest, index);

        }

        //ilk aklan gelen yöntemler, fonksiyonel olmayan :)
        public static void FonksiyonelYontem(int[] src, int[] dest)
        {

            //Öyle bir method yazınki neyi kopyaladığını bilmesin ama kopyalasın.
            //delege türe ne geçebilirim tabiki fonksiyon ismi yani adresi o da içeride çağırsa
            //src'tan deste asalsa demek oldu.
            int index = copyIf(src, dest, NumberUtil.isPrime);
            printArray(dest, index);

            index = copyIf(src, dest, NumberUtil.isEven);
            printArray(dest, index);

            index = copyIf(src, dest, NumberUtil.isOdd);
            printArray(dest, index);

        }

        public static int copyEvenAndDivide3(int [] src, int [] dest)
        {
            int index = 0;

            foreach (int val in src)
                if (NumberUtil.isEven(val) && val % 3 == 0)
                    dest[index++] = val;

            return index;
        }

        public static void printArray(int [] a, int index) 
        {
            for (int i = 0; i < index; ++i)
                Console.Write(a[i] + " ");
            Console.WriteLine();
        }
        //asal kopyaladı
        public static int copyAsal(int [] src, int [] dest)
        {
            int index = 0;

            foreach(int val in src) {
                if(NumberUtil.isPrime(val)) {
                    dest[index++] = val;
                }
            }

            return index;
        }

        //çiftleri
        public static int copyEven(int [] src, int [] dest) 
        {
            int index = 0;

            foreach (int val in src)
                if (NumberUtil.isEven(val))
                    dest[index++] = val;

            return index;
        }

        //tekleri
        public static int copyOdd(int[] src, int[] dest)
        {
            int index = 0;

            foreach (int val in src)
                if (!NumberUtil.isEven(val))
                    dest[index++] = val;

            return index;
        }



        public static void ilkDelegateBildirimleri()
        {
            Console.WriteLine("Hello World!");

            //Delegelerden nesne alabilirim -> Başlangıç methoduna, method adresi verelim ve tutsun
            Proc1 p1 = new Proc1(Console.WriteLine);
            p1("Hello World!");

            //p1(10); int ile çağıramassın tabiki

            //new demeden de atama yapılabildi
            p1 = Console.WriteLine;
            p1("TUGBERK");

            //2 sayının min bulalım
            int result = Math.Min(2, 3);
            Console.WriteLine(result);

            //p2 Math.Min adresini tutuyor.
            Proc2 p2 = Math.Min;
            Console.WriteLine(p2(2, 3));
        }

    }//class

    class Sample {
        public static void delegateBirlesimi()
        {
            Proc3 p3 = Sample.foo;
            p3 += Sample.bar;

            p3();
        }
        public static void foo()
        {
            Console.WriteLine("Sample.foo");
        }

        public static void bar()
        {
            Console.WriteLine("Sample.bar");
        }
    
    }//class

    class NumberUtil {
        //asal
        public static bool isPrime(int val)
        {
            if (val <= 1)
                return false;
            if (val % 2 == 0)
                return val == 2;
            if (val % 3 == 0)
                return val == 3;
            if (val % 5 == 0)
                return val == 5;
            if (val % 7 == 0)
                return val == 7;
            if (val % 11 == 0)
                return val == 11;

            for (int i = 13; i * i <= val; i += 2) 
                if (val % i == 0)
                    return false;

            return true;
        }

        public static bool isEven(int val)
        {
            return val % 2 == 0;
        }

        public static bool isOdd(int val)
        {
            return !isEven(val);
        }
    }

}//namespace
