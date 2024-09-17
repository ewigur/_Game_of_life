/* Redundant Code
    * 
    *     public void LiveOrDead(int x, int y)
   {
       if(RandomPatterns)
       {
           RandomSpawn(x, y);
       }
       else
       {

           myCells[x, y].SetActive(false);
       }
   }
    * 
      public void CellCheck()
   {
       for (int x = 0; x < CellRows; x++)
       {
           for (int y = 0; y < CellColumns; y++) //(Removed nullcheck (!= null) at the end of every if -statement)
           {

               if (x + 1 < CellRows && myCells[x + 1, y])
               {
                   LiveOrDead(x + 1, y);
               }


               if (y + 1 < CellColumns && myCells[x, y + 1])
               {
                   LiveOrDead(x, y + 1);
               }


               if (x + 1 < CellRows && y + 1 < CellColumns && myCells[x + 1, y + 1])
               {
                   LiveOrDead(x + 1, y + 1);
               }


               if (x - 1 >= 0 && myCells[x - 1, y])
               {
                   LiveOrDead(x - 1, y);
               }


               if (y - 1 >= 0 && myCells[x, y - 1])
               {
                   LiveOrDead(x, y - 1);
               }


               if (x - 1 >= 0 && y - 1 >= 0 && myCells[x - 1, y - 1])
               {
                   LiveOrDead(x - 1, y - 1);
               }


               if (x - 1 >= 0 && y + 1 < CellColumns && myCells[x - 1, y + 1])
               {
                   LiveOrDead(x - 1, y + 1);
               }


               if (x + 1 < CellRows && y - 1 >= 0 && myCells[x + 1, y - 1])
               {
                   LiveOrDead(x + 1, y - 1);
               }
           }
       }
   }

    */