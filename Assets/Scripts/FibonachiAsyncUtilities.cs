using System.Threading.Tasks;

namespace MyAsyncUtilites
{
    public static class FibonachiAsyncUtilities
    {
        public static Task<int> FibonachiAsync(int number)
        {
            return Task<int>.Factory.StartNew(()=> Fibonachi(number));
        }

        public static int Fibonachi(int number)
        {
            if (number <1)
                return  0;
            if (number == 1)
                return 1;
            if (number == 2)
                return  1;
            return Fibonachi(number-2) + Fibonachi(number-1);

        }
    }
}