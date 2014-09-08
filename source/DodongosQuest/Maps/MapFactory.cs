using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Maps.Map_Types;

namespace DodongosQuest.Maps
{
    public class MapFactory
    {
        
        public static IMap CreateMap(World world)
        {

                BigRoom _map = new BigRoom();
                _map.CreateBigRoom(world);
                return _map;
            
            
        }
       
     }
}
