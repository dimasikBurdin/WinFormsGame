//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Views
//{
//    public static class MapCreator
//    {
//        private static readonly Dictionary<string, Func<ICreature>> factory = new Dictionary<string, Func<ICreature>>();

//        public static ICreature[,] CreateMap(string map, string separator = "\r\n")
//        {
//            var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
//            if (rows.Select(z => z.Length).Distinct().Count() != 1)
//                throw new Exception($"Wrong test map '{map}'");
//            var result = new ICreature[rows[0].Length, rows.Length];
//            for(int x = 0; x < rows[0].Length; x++)
//            {
//                for(int y = 0; y < rows.Length; y++)
//                {
//                    result[x, y] = CreateMapBySybol(rows[y][x]);
//                }
//            }
//            return result;
//        }

//        private static ICreature CreateMapByTypeSymbol(string namePicture)
//        {

//        }

//        private static ICreature CreateMapBySybol(char charName)
//        {
//            switch(charName)
//            {
//                case 'P':
//                    return CreateMapByTypeSymbol("Player");
//                case 'G':
//                    return CreateMapByTypeSymbol("Grass");
//                case 'T':
//                    return CreateMapByTypeSymbol("Trail");//тропинка
//                case 'L':
//                    return CreateMapByTypeSymbol("Land");//земля
//                case 'R':
//                    return CreateMapByTypeSymbol("Rock");
//                case 'F':
//                    return CreateMapByTypeSymbol("Forest");
//                case ' ':
//                    return null;
//                default:
//                    throw new ArgumentException();
//            }
//        }
//    }
//}
