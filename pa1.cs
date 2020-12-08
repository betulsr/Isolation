
using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        static bool [ , ] board;
        static string nameA;
        static string nameB;
        static int intRow, intCol, rowStartA, colStartA, rowStartB, colStartB;
        static int platformRow, platformCol, platformRowB, platformColB;
        static int moveRowA, moveColA, moveRowB, moveColB;
        static bool playerA = true;
        static string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
    
        static void initialize()
        {
        
            //response user names, default
            WriteLine( "Enter player 1's name: ");
            nameA = ReadLine();
            if (nameA.Length == 0){
				nameA = "Player A";}
            

            WriteLine( "Enter player 2's name: ");
            nameB = ReadLine();
            if (nameB.Length == 0){
            nameB = "Player B";}
        

            //response rows and columns, default
            WriteLine( "Enter # of rows (has to be less than 4 more than 26): ");
            string row = ReadLine();
			if (row.Length == 0 || int.Parse( row ) < 4 || int.Parse( row ) > 26)
            {
				row = "6"; 
			}
            intRow = Convert.ToInt32(row);

        
            WriteLine( "Enter # of columns (has to be less than 4 more than 26: ");
            string column = ReadLine();
            if (column.Length == 0 || int.Parse( column ) < 4 || int.Parse( column ) > 26)
            {
				column = "8"; 
			}
            intCol = Convert.ToInt32(column);
      
            //response platform rows and columns,set default
            WriteLine( nameA + " enter # of rows for your starting position: ");
            string rowA  = ReadLine();
            if (rowA.Length ==0)
            {
                rowA="2";
            }
            rowStartA = Convert.ToInt32(rowA);
            
            WriteLine( nameA + " enter # of columns for your starting position: ");
            string colA  = ReadLine();
            if (colA.Length ==0)
            {
                colA="0";
            }
            colStartA = Convert.ToInt32(colA);
            
            WriteLine( nameB + " enter # of rows for your starting position: ");
            string rowB  = ReadLine();
            if (rowB.Length ==0)
            {
                rowB="3";
            }
            rowStartB = Convert.ToInt32(rowB);
            
            WriteLine( nameA + " enter # of columns for your starting position: ");
            string colB  = ReadLine();
            if (colB.Length ==0)
            {
                colB="7";
            }
            colStartB = Convert.ToInt32(colB);
            
            //place pawn positions on platforms
            platformRow = rowStartA;
            platformCol = colStartA;
            moveRowA = rowStartA;
            moveColA = colStartA;
            platformColB = colStartB;
            platformRowB = rowStartB;
            moveRowB = rowStartB;
            moveColB = colStartB;
            
            //set array
            board = new bool[ intRow, intCol ];
            for (int i = 0; i < board.GetLength(0); i++)
            {
				for ( int j = 0; j < board.GetLength(1); j++)
                {
					board[i,j] = true; 
				}
			}
        }
        static bool makeMove(string move) 
        {
                
                if( move.Length != 4)
                {
                    Write( "You should have entered four characters. ");
                    return false;
                }
                
                    int pawnRow = Array.IndexOf(letters, move.Substring(0,1));
                    int pawnCol = Array.IndexOf(letters, move.Substring(1,1));
                    int removeRow = Array.IndexOf(letters, move.Substring(2,1));
                    int removeCol = Array.IndexOf(letters, move.Substring(3,1));

                    //check all coordinate valid
                    if (pawnRow > board.GetLength(0)-1 || pawnRow < 0 || pawnCol > board.GetLength(1)-1 || pawnCol < 0 || removeRow > board.GetLength(0)-1 || removeRow < 0 || removeCol > board.GetLength(1)-1 || removeCol < 0)
                    {
                        WriteLine("Cannot move there."); 
                        return false;  
                    }
            
                    //check pawn move possible
                    
                    if (playerA)
                    {
                        if (board[pawnRow,pawnCol]==false)
                        {
                            WriteLine("Cannot move here.");
                            return false; 
                            
                        }
                        else if (pawnRow == platformRowB  && pawnCol == platformRowB)
                        {
                            WriteLine("Cannot move here.");
                            return false; 
                        }

                        //check removed tile is possible
                        
                        else if (( removeRow == platformRowB &&  removeCol == platformColB ) || (removeRow == rowStartB  && removeCol == colStartB) || ((removeRow == rowStartB  && removeCol == colStartB)) || (board[removeRow,removeCol] == false) || (removeRow == pawnRow && removeCol == pawnCol))
                        {
                            WriteLine("Cannot remove this tile.");
                            return false;  
                        }
                        moveRowA = pawnRow;
                        moveColA = pawnCol;
                    }
                    else
                    {
                        if (board[pawnRow,pawnCol]==false)
                        {
                            WriteLine("Cannot move here.");
                            return false;    
                        }
                        else if (pawnRow == platformRow  && pawnCol == platformRow)
                        {
                            WriteLine("Cannot move here.");
                            return false; 
                        }
                        

                        //check removed tile is possible
                        else if (( removeRow == platformRow &&  removeCol == platformCol ) || (removeRow == rowStartA  && removeCol == colStartA) || ((removeRow == rowStartB  && removeCol == colStartB)) || (board[removeRow,removeCol] == false) || (removeRow == pawnRow && removeCol == pawnCol))
                        {
                            WriteLine("Cannot remove this tile.");
                            return false;
                        }
                        
                        moveRowB = pawnRow;
                        moveColB = pawnCol;
                    }
                    board[removeRow,removeCol] = false;
                    return true;
        }
        static void DrawGameBoard( )
        {
            Clear();
            const string h  = "\u2500"; // horizontal line
            const string v  = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
            const string sp = " ";      // space
            const string pa = "A";      // pawn A
            const string pb = "B";      // pawn B
            const string bb = "\u25a0"; // block
            const string fb = "\u2588"; // left half block, thicc one
            const string lh = "\u258c"; // left half block
            const string rh = "\u2590"; // right half block
            
            
            // Draw the top board boundary.
            Write( "   " );
            for( int c = 0; c < board.GetLength( 1 ); c ++ )
            {
                Write( "  {0} ", letters[ c ] );
            }
            WriteLine( );
            
            Write( "   " );
            for( int c = 0; c < board.GetLength( 1 ); c ++ )
            {
                if( c == 0 ) Write( tl );
                Write( "{0}{0}{0}", h );
                if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", tr ); 
                else                                Write( "{0}", hb );
            }
            WriteLine( );
            
            // Draw the board rows.
            for( int r = 0; r < board.GetLength( 0 ); r ++ )
            {
                Write( " {0} ", letters[ r ] );
                
                // Draw the row contents.
                for( int c = 0; c < board.GetLength( 1 ); c ++ )
                {
                    if( c == 0 ) Write( v );
                    if( board[ r, c ] )
                    {
                        if( r == moveRowA && c == moveColA ) Write( "{0}{1}", sp + pa + sp, v );
                        else if( r == moveRowB && c == moveColB ) Write( "{0}{1}", sp + pb + sp, v );
                        else if( r == platformRow && c == platformCol ) Write( "{0}{1}", sp + bb + sp, v );
                        else if( r == platformRowB && c == platformColB ) Write( "{0}{1}", sp + bb + sp, v );
                        else Write( "{0}{1}", rh + fb + lh, v );
                    }
                    else
                    {
                        if ((r == rowStartA && c == colStartA) || (r == rowStartB && c == colStartB))
                            Write( "{0}{1}", " "+bb, " "+v );
                        else
                            Write( "{0}{1}", "   ", v );
                    }
                }
                WriteLine( );
                    
            
                // Draw the boundary after the row.
                if( r != board.GetLength( 0 ) - 1 )
                { 
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( vr );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", vl ); 
                        else                                Write( "{0}", hv );
                    }
                    WriteLine( );
                }
                else
                {
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( bl );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", br ); 
                        else                                Write( "{0}", ha );
                    }
                    WriteLine( );
                }
                
            }
        }    
            
        //run
        static void Main(string[] args)
        {
            initialize();
            
            bool run = true; 
            
            while (run)
            {
                DrawGameBoard( );
                if (playerA)
                {
                    WriteLine(nameA+": Enter a move [abcd] ");
                }
                else
                {
                    WriteLine(nameB+": Enter a move [abcd]");
                }
                string move = ReadLine();
                
                while (!makeMove(move))
                {
                    move = ReadLine();
                }
                
                playerA = !playerA;
            }
    

        }
    }
}
