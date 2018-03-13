﻿open System

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

///DATA STRUCTURES 


type Cow = //Does particular cell on the board hold white cow, black cow or is it black
| CW
| CB
| Blank

type Player =
         {
           Cows: int //number of cows lefts during placing
           Turn: bool //player's turn to play?
           Cells : string list
           Who : Cow
         }
type Tile =
      {
           pos: string
           cond: Cow
      }

type GameBoard =  {
       Board: Tile list
       bullets: int*int
    }

type results =          //results state of the current game 
| Mill of GameBoard
| Ongoing of GameBoard
| Winner of Cow * GameBoard
| Draw

let playerB = {Cows = 1 ; Turn = false; Cells =[] ; Who = CB  }//initialisng player information (each startin with twelve cows )
let playerW = {Cows = 1 ; Turn = false; Cells =[] ; Who = CW}


let startBoard =
                 {  Board = [
                              {pos = "a1"; cond = Blank}; {pos = "a3"; cond = Blank}; {pos = "a7"; cond = Blank};
                              {pos = "b2"; cond = Blank}; {pos = "b4"; cond = Blank}; {pos = "b6"; cond = Blank};
                              {pos = "c3"; cond = Blank}; {pos = "c4"; cond = Blank}; {pos = "c5"; cond = Blank};
                              {pos = "d1"; cond = Blank}; {pos = "d2"; cond = Blank}; {pos = "d3"; cond = Blank};
                              {pos = "d5"; cond = Blank}; {pos = "d6"; cond = Blank}; {pos = "d7"; cond = Blank};
                              {pos = "e3"; cond = Blank}; {pos = "e4"; cond = Blank}; {pos = "e5"; cond = Blank};
                              {pos = "f2"; cond = Blank}; {pos = "f4"; cond = Blank}; {pos = "f6"; cond = Blank};
                              {pos = "g1"; cond = Blank}; {pos = "g4"; cond = Blank}; {pos = "g7"; cond = Blank};
                           ]
                    bullets = (12,12)
                 }

let mills = [
             ("a1", "a2", "a3");
             ("b2", "b4", "b6");
             ("c3","c4","c5");
             ("d1","d2","d3");
             ("d5","d6", "d7");
             ("e3","e4", "e5");
             ("f2","f4", "f6");
             ("g1","g4", "g7");
             ("a1","d1", "g1");
             ("b2","d2", "f2");
             ("c3","d3", "e3");
             ("a4","b4", "c4");
             ("e4","f4", "g4");
             ("c5","d5", "e5");
             ("b6","d6", "f6");
             ("a7","d7", "g7");                                                 
             ("a1","b2", "c3");
             ("a7","b6", "c5");
             ("g1","f2", "e3");
             ("g7","f6", "e5");
            ]





//FUNCTIONS
let printBoard ( (r1, r2, r3, r4, r5, r6, r7)) = //printing the board onto the screen. as the user will see it.
      //System.Console.Clear()
      let liz = "_____" //5
      let liz2 = "____" //4
      let bk = "     " //5
      let bk2 = "    "//4
      let printSep1 () = printfn  "     |               |               |\n     |               |               |\n     |               |               |" //board design 
      let printSep2 () = printfn  "     |         |           |         |\n     |         |           |         |\n     |         |           |         |"
      let cell value = 
          match value with 
          | CB -> "B"
          | CW -> "W"
          | Blank -> "O"
 
      
      let (a1,a2,a3,a4,a5,a6,a7)  = r1
      let (b1,b2,b3,b4,b5,b6,b7)  = r2
      let (c1,c2,c3,c4,c5,c6,c7)  = r3
      let (d1,d2,d3,d4,d5,d6,d7)  = r4
      let (e1,e2,e3,e4,e5,e6,e7)  = r5
      let (f1,f2,f3,f4,f5,f6,f7)  = r6
      let (g1,g2,g3,g4,g5,g6,g7)  = r7
           
          (* printfn "The status is %b" test // Test example of input testing method
           printOutValidCoordinates // Test example of whether valid an input is 
           printfn "The coordinates are %A" (actCoardinates)*)
           //let cell = cell offset
           //let sym = "O"
      printfn "     %d%s%d%s%d%s%d%s%d%s%d%s%d " 1 bk2 2 bk2 3 bk 4 bk 5 bk2 6 bk2 7  // prints out the number scale at the top of the board
      printfn "\n"
      // rest of the methods called prints out the board, one line at a time.
      printfn "A    %s%s%s%s%s%s%s%s%s%s%s%s%s " (cell a1 ) liz ("") liz ("") liz (cell a4 ) liz ("") liz ("") liz (cell a7 ) 
      printSep1()
      printfn "B    %s%s%s%s%s%s%s%s%s%s%s%s%s " ("") liz (cell b2 ) liz ("") liz (cell b4 ) liz ("") liz (cell b6 ) liz ("") 
      printSep1()
      printfn "C    %s%s%s%s%s%s%s%s%s%s%s%s%s " ("") bk ("") bk (cell c3 ) liz (cell c4 ) liz (cell c5 ) bk ("") bk ("")
      printSep2()
      printfn "D    %s%s%s%s%s%s%s%s%s%s%s%s%s " (cell d1 ) liz2 (cell d2 ) liz2 (cell d3 ) bk bk (" ") (cell d5 ) liz2 (cell d6 ) liz2 (cell d7 )
      printSep2()
      printfn "E    %s%s%s%s%s%s%s%s%s%s%s%s%s " ("") bk ("") bk (cell e3 ) liz (cell e4 ) liz (cell e5 ) bk ("") bk ("")
      printSep1()
      printfn "F    %s%s%s%s%s%s%s%s%s%s%s%s%s " ("") liz (cell f2 ) liz ("") liz (cell f4 ) liz ("") liz (cell f6 ) liz ("")
      printSep1()
      printfn "G    %s%s%s%s%s%s%s%s%s%s%s%s%s " (cell g1 ) liz ("") liz ("") liz (cell g4 ) liz ("") liz ("") liz (cell g7 ) 
      let printSepConners () = printfn "|\%s|%s/|" bk bk    
      printfn ""
         



let clearboard()=System.Console.Clear()
let blankBoard =
    let blankRow = Blank, Blank, Blank, Blank, Blank, Blank, Blank
    Board (blankRow, blankRow, blankRow, blankRow, blankRow, blankRow, blankRow)

let swapPlayer x= 
      match x with 
      | CB -> CW
      | CW -> CB
      | Blank -> failwith "A FATAL ERROR OCCURED"

let inputCheck (coordinate:string) =       //validating user input
      let n = coordinate.ToUpper ()
      match String.length (n) with
      | 2 -> 
            match Char.IsLetter(n.[0]) && n.[0]<'g' with 
            | true -> 
                  match Char.IsDigit(n.[1]) &&  ((Int32.Parse(string n.[1]) >= 0) && Int32.Parse(string n.[1]) < 8)  with
                      | true -> true
                      | _ -> false
            | _ -> false
      | _ -> false

let isBlank game position = //check if the cell is black
    match position, game with
    | "A1", Board((Blank,_,_,_,_,_,_),_,_,_,_,_,_) -> true
    | "A4", Board((_,_,_,Blank,_,_,_),_,_,_,_,_,_) -> true
    | "A7", Board((_,_,_,_,_,_,Blank),_,_,_,_,_,_) -> true
    | "B2", Board(_,(_,Blank,_,_,_,_,_),_,_,_,_,_) -> true
    | "B4", Board(_,(_,_,_,Blank,_,_,_),_,_,_,_,_) -> true
    | "B6", Board(_,(_,_,_,_,_,Blank,_),_,_,_,_,_) -> true
    | "C3", Board(_,_,(_,_,Blank,_,_,_,_),_,_,_,_) -> true
    | "C4", Board(_,_,(_,_,_,Blank,_,_,_),_,_,_,_) -> true
    | "C5", Board(_,_,(_,_,_,_,Blank,_,_),_,_,_,_) -> true
    | "D1", Board(_,_,_,(Blank,_,_,_,_,_,_),_,_,_) -> true
    | "D2", Board(_,_,_,(_,Blank,_,_,_,_,_),_,_,_) -> true
    | "D3", Board(_,_,_,(_,_,Blank,_,_,_,_),_,_,_) -> true
    | "D5", Board(_,_,_,(_,_,_,_,Blank,_,_),_,_,_) -> true
    | "D6", Board(_,_,_,(_,_,_,_,_,Blank,_),_,_,_) -> true
    | "D7", Board(_,_,_,(_,_,_,_,_,_,Blank),_,_,_) -> true
    | "E3", Board(_,_,_,_,(_,_,Blank,_,_,_,_),_,_) -> true
    | "E4", Board(_,_,_,_,(_,_,_,Blank,_,_,_),_,_) -> true
    | "E5", Board(_,_,_,_,(_,_,_,_,Blank,_,_),_,_) -> true
    | "F2", Board(_,_,_,_,_,(_,Blank,_,_,_,_,_),_) -> true
    | "F4", Board(_,_,_,_,_,(_,_,_,Blank,_,_,_),_) -> true
    | "F6", Board(_,_,_,_,_,(_,_,_,_,_,Blank,_),_) -> true
    | "G1", Board(_,_,_,_,_,_,(Blank,_,_,_,_,_,_)) -> true
    | "G4", Board(_,_,_,_,_,_,(_,_,_,Blank,_,_,_)) -> true
    | "G7", Board(_,_,_,_,_,_,(_,_,_,_,_,_,Blank)) -> true
    | _ -> false 

let isMill game position = //check if the cell is black
    match  game with
    | Board(_,_,_,_,_,(_,CB,_,CB,_,CB,_),_) -> true 
    | Board((CB,_,_,CB,_,_,CB),_,_,_,_,_,_) -> true 
    | Board(_,(_,CB,_,CB,_,CB,_),_,_,_,_,_) -> true 
    | Board(_,_,(_,_,CB,CB,CB,_,_),_,_,_,_) -> true 
    | Board(_,_,_,(CB,CB,CB,_,_,_,_),_,_,_) -> true 
    | Board(_,_,_,(_,_,_,_,CB,CB,CB),_,_,_) -> true 
    | Board(_,_,_,_,(_,_,CB,CB,CB,_,_),_,_) -> true 
    | Board(_,_,_,_,_,_,(CB,_,_,CB,_,_,CB)) -> true  
    | Board((CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_)) ->true  
    | Board(_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_) ->true  
    | Board(_,_,(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_) ->true  
    | Board((_,_,_,CB,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_,_,_) ->true  
    | Board(_,_,_,_,(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_)) ->true  
    | Board(_,_,(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),_,_) ->true  
    | Board(_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_) ->true  
    | Board((_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB)) ->true  
    | Board((CB,_,_,_,_,_,_), (_,CB,_,_,_,_,_), (_,_,CB,_,_,_,_),_,_,_,_) -> true
    | Board((_,_,_,_,_,_,CB), (_,_,_,_,_,CB,_), (_,_,_,_,CB,_,_),_,_,_,_) -> true
    | Board(_,_,_,_,(_,_,CB,_,_,_,_),(_,CB,_,_,_,_,_), (CB,_,_,_,_,_,_)) ->true 
    | Board(_,_,_,_,(_,_,_,_,CB,_,_),(_,_,_,_,_,CB,_), (_,_,_,_,_,_,CB)) -> true
    | Board(_,_,_,_,_,(_,CB,_,CB,_,CB,_),_) -> true                                               
    | Board((CB,_,_,CB,_,_,CB),_,_,_,_,_,_) -> true 
    | Board(_,(_,CB,_,CB,_,CB,_),_,_,_,_,_) -> true 
    | Board(_,_,(_,_,CB,CB,CB,_,_),_,_,_,_) -> true 
    | Board(_,_,_,(CB,CB,CB,_,_,_,_),_,_,_) -> true 
    | Board(_,_,_,(_,_,_,_,CB,CB,CB),_,_,_) -> true 
    | Board(_,_,_,_,(_,_,CB,CB,CB,_,_),_,_) -> true 
    | Board(_,_,_,_,_,_,(CB,_,_,CB,_,_,CB)) -> true      
    | Board((CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_)) ->  true 
    | Board(_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_) ->  true 
    | Board(_,_,(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_) ->  true 
    | Board((_,_,_,CB,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_,_,_) ->  true 
    | Board(_,_,_,_,(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_)) ->  true 
    | Board(_,_,(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),_,_) ->  true 
    | Board(_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_) ->  true 
    | Board((_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB)) ->  true 
    | Board((CB,_,_,_,_,_,_), (_,CB,_,_,_,_,_), (_,_,CB,_,_,_,_),_,_,_,_) ->true 
    | Board((_,_,_,_,_,_,CB), (_,_,_,_,_,CB,_), (_,_,_,_,CB,_,_),_,_,_,_) -> true
    | Board(_,_,_,_,(_,_,CB,_,_,_,_),(_,CB,_,_,_,_,_), (CB,_,_,_,_,_,_)) ->true  
    | Board(_,_,_,_,(_,_,_,_,CB,_,_),(_,_,_,_,_,CB,_), (_,_,_,_,_,_,CB)) ->true      
    | _ -> false
    

//Update the mill status 

//Break up the mill checks into smaller mills
(*let mutable BLine1 = 0
let mutable BLine2 = 0
let mutable BLine3 = 0
let mutable BLine4 = 0
let mutable BLine5 = 0 
let mutable BLine6 = 0 
let mutable BLine7 = 0 
let mutable BLine8 = 0 *)


   (*match BLine1 = 1 || BLine2 =1|| BLine3=1 || BLine4=1 || BLine5=1 || BLine6=1 || BLine7=1 || BLine8 =1 with
   | true -> true
   | _ -> false *)

(*let mutable BLine11 = 0
let mutable BLine12 = 0
let mutable BLine13 = 0
let mutable BLine14 = 0
let mutable BLine15 = 0 
let mutable BLine16 = 0 
let mutable BLine17 = 0 
let mutable BLine18 = 0*)


    (*match BLine11=1 || BLine12=1 || BLine13=1 || BLine14=1 || BLine15=1 || BLine16=1 || BLine17=1 || BLine18=1  with 
    | true -> true
    | _ -> false *)

(*let mutable Bdiagonal1 = 0
let mutable Bdiagonal2 = 0
let mutable Bdiagonal3 = 0
let mutable Bdiagonal4 = 0*)

   
   (* match Bdiagonal1=1 || Bdiagonal2=2 || Bdiagonal3=1 || Bdiagonal4=1 with
         | true -> true
         | _ -> false *)

///White Cows

//Break up the mill checks into smaller mills
(*let mutable WLine1 = 0
let mutable WLine2 = 0
let mutable WLine3 = 0
let mutable WLine4 = 0
let mutable WLine5 = 0 
let mutable WLine6 = 0 
let mutable WLine7 = 0 
let mutable WLine8 = 0*)



  (* match WLine1 = 1 || WLine2 =1|| WLine3=1 || WLine4=1 || WLine5=1 || WLine6=1 || WLine7=1 || WLine8 =1 with
   | true -> true
   | _ -> false *)

(*let mutable WLine11 = 0
let mutable WLine12 = 0
let mutable WLine13 = 0
let mutable WLine14 = 0
let mutable WLine15 = 0 
let mutable WLine16 = 0 
let mutable WLine17 = 0 
let mutable WLine18 = 0 *)


     (*match WLine11=1 || WLine12=1 || WLine13=1 || WLine14=1 || WLine15=1 || WLine16=1 || WLine17=1 || WLine18=1  with 
    | true -> true
    | _ -> false *)

(*let mutable Wdiagonal1 = 0
let mutable Wdiagonal2 = 0
let mutable Wdiagonal3 = 0
let mutable Wdiagonal4 = 0 *)


let MillCheck1 game  = 
   let test =  
       match game with 
          | Board(_,_,_,_,_,(_,CB,_,CB,_,CB,_),_) -> 1 //&BLine1 //Line f    
          | Board((CB,_,_,CB,_,_,CB),_,_,_,_,_,_) -> 1  //&BLine2  //Line a
          | Board(_,(_,CB,_,CB,_,CB,_),_,_,_,_,_) -> 1  //&BLine3 //Line/Mill b
          | Board(_,_,(_,_,CB,CB,CB,_,_),_,_,_,_) -> 1  //&BLine4 //Line/Mill c
          | Board(_,_,_,(CB,CB,CB,_,_,_,_),_,_,_) -> 1  //&BLine5 //Line/Mill d
          | Board(_,_,_,(_,_,_,_,CB,CB,CB),_,_,_) -> 1  //&BLine6 //Line/Mill d
          | Board(_,_,_,_,(_,_,CB,CB,CB,_,_),_,_) -> 1  //&BLine7//Line/Mill e
          | Board(_,_,_,_,_,_,(CB,_,_,CB,_,_,CB)) -> 1  //&BLine8 //Line/Mill g
          | _ -> 0
   test

let MillCheck2 game = 
    let test = 
       match game with 
          | Board((CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_)) ->1 //&BLine11 // Line/Mill Column 1
          | Board(_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_) ->1 //&BLine12 //Line/Mill Column 2
          | Board(_,_,(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_) ->1 //&BLine13 // Line/Mill Column 3
          | Board((_,_,_,CB,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_,_,_) ->1//&BLine14 //Line/Mill Column 4
          | Board(_,_,_,_,(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_)) ->1//&BLine15//Line/Mill Column 4
          | Board(_,_,(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),_,_) ->1 //&BLine16 //Line/Mill Column 5
          | Board(_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_) ->1//&BLine17 // Line/Mill Column 6
          | Board((_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB)) ->1 //&BLine18 // Line/Mill Column 7
          | _ ->0
    test

let MillCheck3 (game:GameBoard) =
    let test = 
          match game with
          | Board((CB,_,_,_,_,_,_), (_,CB,_,_,_,_,_), (_,_,CB,_,_,_,_),_,_,_,_) ->1 //&Bdiagonal1 // Line/Mill Bdiagonal A-C 
          | Board((_,_,_,_,_,_,CB), (_,_,_,_,_,CB,_), (_,_,_,_,CB,_,_),_,_,_,_) ->1 //&Bdiagonal2 //Line/Mill Bdiagonal C-A
          | Board(_,_,_,_,(_,_,CB,_,_,_,_),(_,CB,_,_,_,_,_), (CB,_,_,_,_,_,_)) -> 1 //&Bdiagonal3 //Line/Mill Bdiagonal G-E
          | Board(_,_,_,_,(_,_,_,_,CB,_,_),(_,_,_,_,_,CB,_), (_,_,_,_,_,_,CB)) -> 1 //&Bdiagonal4 //Line/Mill Bdiagonal E-G
          | _ -> 0
    test

let MillCheck4 game  = 
   let test =  
       match game with 
          | Board(_,_,_,_,_,(_,CB,_,CB,_,CB,_),_) -> 1 //&WLine1 //Line f    
          | Board((CB,_,_,CB,_,_,CB),_,_,_,_,_,_) -> 1 //&WLine2  //Line a
          | Board(_,(_,CB,_,CB,_,CB,_),_,_,_,_,_) -> 1 //&WLine3 //Line/Mill b
          | Board(_,_,(_,_,CB,CB,CB,_,_),_,_,_,_) -> 1 //&WLine4 //Line/Mill c
          | Board(_,_,_,(CB,CB,CB,_,_,_,_),_,_,_) -> 1 //&WLine5 //Line/Mill d
          | Board(_,_,_,(_,_,_,_,CB,CB,CB),_,_,_) -> 1 //&WLine6 //Line/Mill d
          | Board(_,_,_,_,(_,_,CB,CB,CB,_,_),_,_) -> 1 //&WLine7//Line/Mill e
          | Board(_,_,_,_,_,_,(CB,_,_,CB,_,_,CB)) -> 1 //&WLine8 //Line/Mill g
          | _ -> 0
   test

let MillCheck5 game = 
    let test = 
       match game with 
          | Board((CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_),_,_,(CB,_,_,_,_,_,_)) ->1 // &WLine11 // Line/Mill Column 1
          | Board(_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_,(_,CB,_,_,_,_,_),_) ->1 //&WLine12 //Line/Mill Column 2
          | Board(_,_,(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_) ->1 //&WLine13 // Line/Mill Column 3
          | Board((_,_,_,CB,_,_,_),(_,_,CB,_,_,_,_),(_,_,CB,_,_,_,_),_,_,_,_) ->1//&WLine14 //Line/Mill Column 4
          | Board(_,_,_,_,(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_),(_,_,_,CB,_,_,_)) ->1//&WLine15//Line/Mill Column 4
          | Board(_,_,(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),(_,_,_,_,CB,_,_),_,_) ->1 //&WLine16 //Line/Mill Column 5
          | Board(_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_,(_,_,_,_,_,CB,_),_) ->1//&WLine17 // Line/Mill Column 6
          | Board((_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB),_,_,(_,_,_,_,_,_,CB)) ->1 //&WLine18 // Line/Mill Column 7
          | _ -> 0
    test

let MillCheck6 (game:GameBoard) =
    let test = 
          match game with
          | Board((CB,_,_,_,_,_,_), (_,CB,_,_,_,_,_), (_,_,CB,_,_,_,_),_,_,_,_) -> 1 //&Wdiagonal1 // Line/Mill Wdiagonal A-C 
          | Board((_,_,_,_,_,_,CB), (_,_,_,_,_,CB,_), (_,_,_,_,CB,_,_),_,_,_,_) -> 1 //&Wdiagonal2 //Line/Mill Wdiagonal C-A
          | Board(_,_,_,_,(_,_,CB,_,_,_,_),(_,CB,_,_,_,_,_), (CB,_,_,_,_,_,_)) ->  1 //&Wdiagonal3 //Line/Mill Wdiagonal G-E
          | Board(_,_,_,_,(_,_,_,_,CB,_,_),(_,_,_,_,_,CB,_), (_,_,_,_,_,_,CB)) ->  1 //&Wdiagonal4 //Line/Mill Wdiagonal E-G
          | _ ->0 //mills closed here
    test

let totalMillCheck game =
     match (MillCheck1 game) = 1 ||(MillCheck1 game) = 1  ||(MillCheck1 game) = 1  ||(MillCheck1 game) = 1 with
         | true -> true
         | _ -> false 
         
let invertMillCheck game = 
       let bind =
          match (MillCheck1 game) = 1 with
          | true -> false
          | _ -> false
       let bind2 =
          match (MillCheck2 game) = 1 with
          | true -> false
          | _ -> false
       let bind3 = 
          match (MillCheck3 game) = 1 with
          | true -> false
          | _ -> false
       let bind4 =
          match (MillCheck4 game) = 1 with
          | true -> false
          | _ -> false
       bind4
//let test = inputCheck "f6" 
//let test2 = isBlank ga
let listChars = ["a"; "b"; "c";"d";"e";"f"]
let coardinates index = [for i in 1.. 7-> string (listChars.[index]+string (i))]
//Dynamic List of all the possible coordinates, inputs will thus be matched to this list 
let actCoardinates = [for i in 0.. (listChars.Length-1) -> coardinates i]

// Tester method to run through the 2D array of input values...
let printOutValidCoordinates = 
          let rec innerHelp (input: string list list) index = 
              match input with
              | actCoardinates -> 
                   match (index) < 6 with
                   |true ->
                      let tmp = actCoardinates.[index]
                      for i in tmp do
                        printfn "%s status is: %b\t" i (inputCheck i)
                      printfn "\n"
                      innerHelp input (index+1)
                   | _ -> printfn "Done"
              //| [[]]-> printfn "Not correct input" 
          innerHelp actCoardinates 0  
let Check (game: GameBoard)  =
      match totalMillCheck game with
      | true -> Mill game  
      | _ -> Ongoing game               

let makeMove symbol (Board (r1, r2,r3,r4,r5,r6,r7)) pos = 
       let newBoard =       //creating a new (updated) board
         let changeCol col (a,b,c,d,e,f,g) = 
            match col with 
            | 0 -> symbol,b,c,d,e,f,g
            | 1 -> a,symbol,c,d,e,f,g
            | 2 -> a,b,symbol,d,e,f,g
            | 3 -> a,b, c, symbol,e,f,g
            | 4 -> a,b,c,d,symbol,f,g
            | 5 -> a,b,c,d,e,symbol,g
            | 6 -> a,b,c,d,e,f,symbol
            | _ -> failwith "Error occured"
         let data = 
             match pos with
             | "A1" -> changeCol 0 r1, r2,r3,r4,r5,r6,r7
             | "A4" -> changeCol 3 r1, r2,r3,r4,r5,r6,r7
             | "A7" -> changeCol 6 r1, r2,r3,r4,r5,r6,r7
             | "B2" -> r1, changeCol 1 r2,r3,r4,r5,r6,r7
             | "B4" -> r1, changeCol 3 r2,r3,r4,r5,r6,r7
             | "B6" -> r1, changeCol 5 r2,r3,r4,r5,r6,r7
             | "C3" -> r1, r2, changeCol 2 r3,r4,r5,r6,r7
             | "C4" -> r1, r2, changeCol 3 r3,r4,r5,r6,r7
             | "C5" -> r1, r2, changeCol 4 r3,r4,r5,r6,r7
             | "D1" -> r1, r2,r3,changeCol 0 r4,r5,r6,r7
             | "D2" -> r1, r2,r3, changeCol 1 r4,r5,r6,r7
             | "D3" -> r1, r2,r3, changeCol 2 r4,r5,r6,r7
             | "D5" -> r1, r2,r3, changeCol 4 r4,r5,r6,r7
             | "D6" -> r1, r2,r3,changeCol 5 r4,r5,r6,r7
             | "D7" -> r1, r2,r3, changeCol 6 r4,r5,r6,r7
             | "E3" -> r1, r2,r3,r4, changeCol 2 r5,r6,r7
             | "E4" -> r1, r2,r3,r4,changeCol 3 r5,r6,r7
             | "E5" -> r1, r2,r3,r4,changeCol 4 r5,r6,r7
             | "F2" -> r1, r2,r3,r4,r5, changeCol 1 r6,r7
             | "F4" ->  r1, r2,r3,r4,r5,changeCol 3 r6,r7
             | "F6" ->  r1, r2,r3,r4,r5, changeCol 5 r6,r7
             | "G1" ->  r1, r2,r3,r4,r5,r6, changeCol 0 r7
             | "G4" ->  r1, r2,r3,r4,r5, r6, changeCol 3 r7
             | "G7" ->  r1, r2,r3,r4,r5,r6, changeCol 6 r7
             | _ -> failwith "error occured in changing columns"
         Board data
       Check newBoard
// Print out board in destroy before anything else
// Get inputs from this method

let notown pos =            //makes sure you dont destroy your own cow
    match ((List.contains pos playerB.Cells) && playerB.Turn = true) || ((List.contains pos playerW.Cells) && playerW.Turn = true) with 
    |true -> true
    |_ -> false
    
    

let DestroyPiece (Board (r1, r2,r3,r4,r5,r6,r7))  = 
     //printfn "Which cow would you like to destroy: "
     let n = Console.ReadLine()
     let pos = n.ToUpper ()
     match (inputCheck pos = true)  with //&& (notown pos = true) with 
        |true ->  
             let newBoard =             //Creates a new (updated) board with deleted cow
                 let changeCol col (a,b,c,d,e,f,g) = 
                    match col with 
                    | 0 -> Blank,b,c,d,e,f,g
                    | 1 -> a,Blank,c,d,e,f,g
                    | 2 -> a,b,Blank,d,e,f,g
                    | 3 -> a,b, c, Blank,e,f,g
                    | 4 -> a,b,c,d,Blank,f,g
                    | 5 -> a,b,c,d,e,Blank,g
                    | 6 -> a,b,c,d,e,f,Blank
                    | _ -> failwith "Error occured"
                 let data = 
                     match pos with
                     | "A1" -> changeCol 0 r1, r2,r3,r4,r5,r6,r7
                     | "A4" -> changeCol 3 r1, r2,r3,r4,r5,r6,r7
                     | "A7" -> changeCol 6 r1, r2,r3,r4,r5,r6,r7
                     | "B2" -> r1, changeCol 1 r2,r3,r4,r5,r6,r7
                     | "B4" -> r1, changeCol 3 r2,r3,r4,r5,r6,r7
                     | "B6" -> r1, changeCol 5 r2,r3,r4,r5,r6,r7
                     | "C3" -> r1, r2, changeCol 2 r3,r4,r5,r6,r7
                     | "C4" -> r1, r2, changeCol 3 r3,r4,r5,r6,r7
                     | "C5" -> r1, r2, changeCol 4 r3,r4,r5,r6,r7
                     | "D1" -> r1, r2,r3,changeCol 0 r4,r5,r6,r7
                     | "D2" -> r1, r2,r3, changeCol 1 r4,r5,r6,r7
                     | "D3" -> r1, r2,r3, changeCol 2 r4,r5,r6,r7
                     | "D5" -> r1, r2,r3, changeCol 4 r4,r5,r6,r7
                     | "D6" -> r1, r2,r3,changeCol 5 r4,r5,r6,r7
                     | "D7" -> r1, r2,r3, changeCol 6 r4,r5,r6,r7
                     | "E3" -> r1, r2,r3,r4, changeCol 2 r5,r6,r7
                     | "E4" -> r1, r2,r3,r4,changeCol 3 r5,r6,r7
                     | "E5" -> r1, r2,r3,r4,changeCol 4 r5,r6,r7
                     | "F2" -> r1, r2,r3,r4,r5, changeCol 1 r6,r7
                     | "F4" ->  r1, r2,r3,r4,r5,changeCol 3 r6,r7
                     | "F6" ->  r1, r2,r3,r4,r5, changeCol 5 r6,r7
                     | "G1" ->  r1, r2,r3,r4,r5,r6, changeCol 0 r7
                     | "G4" ->  r1, r2,r3,r4,r5, r6, changeCol 3 r7
                     | "G7" ->  r1, r2,r3,r4,r5,r6, changeCol 6 r7
                     | _ -> failwith "error occured in changing columns"
                 Board data
             newBoard
        |_ -> failwith "Cannot destroy own cow"
                //printfn "Cannot detroy own cow" 
let own pos = 
    match ((List.contains pos playerB.Cells) && playerB.Turn = true) || ((List.contains pos playerW.Cells) && playerW.Turn = true) with //makes sure you dont move another player's cow
    |true -> true
    |_ -> false

let movingCow (Board (r1, r2,r3,r4,r5,r6,r7)) = // start moving cow once number of cows on the side = 0
     //printfn "Which cow would you like to move [<Letter><Number>]: "
     let pos = Console.ReadLine() 
     match (inputCheck pos = true) && (own pos = true) with 
        |true ->  
             let newBoard =             //Creates a new (updated) board with deleted cow
                 let changeCol col (a,b,c,d,e,f,g) = 
                    match col with 
                    | 0 -> Blank,b,c,d,e,f,g
                    | 1 -> a,Blank,c,d,e,f,g
                    | 2 -> a,b,Blank,d,e,f,g
                    | 3 -> a,b, c, Blank,e,f,g
                    | 4 -> a,b,c,d,Blank,f,g
                    | 5 -> a,b,c,d,e,Blank,g
                    | 6 -> a,b,c,d,e,f,Blank
                    | _ -> failwith "Error occured"
                 let data = 
                     match pos with
                     | "A1" -> changeCol 0 r1, r2,r3,r4,r5,r6,r7
                     | "A4" -> changeCol 3 r1, r2,r3,r4,r5,r6,r7
                     | "A7" -> changeCol 6 r1, r2,r3,r4,r5,r6,r7
                     | "B2" -> r1, changeCol 1 r2,r3,r4,r5,r6,r7
                     | "B4" -> r1, changeCol 3 r2,r3,r4,r5,r6,r7
                     | "B6" -> r1, changeCol 5 r2,r3,r4,r5,r6,r7
                     | "C3" -> r1, r2, changeCol 2 r3,r4,r5,r6,r7
                     | "C4" -> r1, r2, changeCol 3 r3,r4,r5,r6,r7
                     | "C5" -> r1, r2, changeCol 4 r3,r4,r5,r6,r7
                     | "D1" -> r1, r2,r3,changeCol 0 r4,r5,r6,r7
                     | "D2" -> r1, r2,r3, changeCol 1 r4,r5,r6,r7
                     | "D3" -> r1, r2,r3, changeCol 2 r4,r5,r6,r7
                     | "D5" -> r1, r2,r3, changeCol 4 r4,r5,r6,r7
                     | "D6" -> r1, r2,r3,changeCol 5 r4,r5,r6,r7
                     | "D7" -> r1, r2,r3, changeCol 6 r4,r5,r6,r7
                     | "E3" -> r1, r2,r3,r4, changeCol 2 r5,r6,r7
                     | "E4" -> r1, r2,r3,r4,changeCol 3 r5,r6,r7
                     | "E5" -> r1, r2,r3,r4,changeCol 4 r5,r6,r7
                     | "F2" -> r1, r2,r3,r4,r5, changeCol 1 r6,r7
                     | "F4" ->  r1, r2,r3,r4,r5,changeCol 3 r6,r7
                     | "F6" ->  r1, r2,r3,r4,r5, changeCol 5 r6,r7
                     | "G1" ->  r1, r2,r3,r4,r5,r6, changeCol 0 r7
                     | "G4" ->  r1, r2,r3,r4,r5, r6, changeCol 3 r7
                     | "G7" ->  r1, r2,r3,r4,r5,r6, changeCol 6 r7
                     | _ -> failwith "error occured in changing columns"
                 Board data
             newBoard
        |_ ->  failwith "error occured in changing columns"



(*let cowsleft cows = 
    match cows with  //(playerW.Cows < 1  && playerW.Turn = true) with
    |0 -> false // no more cows to place, starting moving
    |_ -> true *)
    
let rec run player game  =
    // need to find the blank cells that can be used...
    clearboard()
    printBoard game                      
    run player game// cows 
    printfn "%A's turn.  Type the Co-ordinates [<LETTER><NUMBER>] of the cell that you want to play into." player

    let b = System.Console.ReadLine() // Co-ordinate for cow from user
    let n = b.ToUpper ()
        //updateplayer player n
    match n with
    | "A1"  | "A4" | "A7" | "a7" | "B2" |"B4"  | "B6"  | "C3" | "C4" | "C5"  | "D1" | "D2" | "D3" | "D5" | "D6" | "D7" | "E3" | "E4" | "E5" | "F2" | "F4" | "F6" | "G1" | "G4" | "G7"  ->
       match isBlank game n with
        | true -> 
              printfn "Invalid Input, Please re-enter position" 
              makeMove player game   //Shoot n game player    
        | _ ->
             printfn "Invalid Input, Please re-enter position" 
             run player game //cows
               
    | _ -> run player game //cows
// Prints out the board based on row values

let rules() = printfn ("The game contains 3 stages
Stage 1: Cow placing
•	Each player has 12 pieces known as cows. Player 1 has dark cow and Player 2 has light cows
•	Player one moves first
•	Each turn consists of placing cows on the board 
•	Three cows on any line creates a mill
•	Whenever a player creates a mill they are to shoot any cow of the opponent accept cows in a mill.
•	One cow can be short per turn even if a turn creates more than one mill

Stage 2: Cow moving
•	After players run out of cows to place, each turn consists of moving cows to an empty adjacent intersection.
•	A mill allows a cow that is not in a mill to be short, unless all cows are in a mill
•	A mill can be broken and remade by moving cows back and forth.
•	A mill broken to make another mill can only be remade after the next turn

Stage 3: flying
•	Whenever a player has three cows left, the player’s cows are allowed to fly to any intersection not just the adjacent ones.
Finishing the game
•	A player wins when the opponent cannot make a move
•	A player wins when the opponent is left with 2 cows
•	When both players have three cows, they are allowed ten turns. If no shooting takes place it is declared a draw.
•	One that cheats loses the game. \n") 

clearboard ()
rules()
printfn " Please press [P] to play the game!"
let getinput()  = (System.Console.ReadKey true).KeyChar
 
let updateplayer p n = 
    match p with 
    |CB -> {Cows = 0 ; Turn = true; Cells = n::playerB.Cells ; Who = CB  }
    |CW -> {Cows = 0 ; Turn = true; Cells = n::playerW.Cells ; Who = CW  }
    |_ -> failwith "Invalid input" 


(*let rec runGame currentPlayer game  =
    let playAgain () =
        printfn "Play again? [y/N] "
        match System.Console.ReadLine() with
        | "Y" | "y" -> runGame CB blankBoard
        | _ -> ()
 
    match run currentPlayer game with
    |Ongoing newBoard -> 
         //updateplayer currentPlayer n 
         runGame (swapPlayer currentPlayer) newBoard 
    |Mill newBoard ->
         printfn "Enter position of cow you'd like to destroy [<Letter><Number>]: "
         let var = DestroyPiece newBoard 
         runGame (swapPlayer currentPlayer) var*)
let rec runGame currentPlayer game   =
    let playAgain () =
        printfn "Play again? [y/N] "
        match System.Console.ReadLine() with
        | "Y" | "y" -> runGame CB blankBoard //(cows-1)
        | _ -> ()
   (* match cows =0 with
    |true ->  
            match run currentPlayer game  with
            |Ongoing newBoard  -> 
                 printfn "Enter position of cow you'd like to move"
                 let move = movingCow newBoard
                 runGame (swapPlayer currentPlayer) move  0
                 printfn "Enter position where you would like to place the cow "
                 runGame (swapPlayer currentPlayer) newBoard 0
            |Mill newBoard ->
                 printfn "Enter position of cow you'd like to destroy: "
                 let var = DestroyPiece newBoard 
                 runGame (swapPlayer currentPlayer) var  0

            |Winner (player, board) ->
                printBoard board
                printfn "Winner is %A" player
                playAgain ()
            |Draw ->
               printfn "Draaaaw"
               playAgain ()
    | _ ->*)
        match run currentPlayer game  with
        |Ongoing newBoard -> runGame (swapPlayer currentPlayer) newBoard //(cows-1)
        |Mill newBoard ->
             printfn "Enter position of cow you'd like to destroy: "
             let var = DestroyPiece newBoard 
             runGame (swapPlayer currentPlayer) var  //(cows-1)

        |Winner (player, board) ->
            printBoard board
            printfn "Winner is %A" player
            playAgain ()
        |Draw ->
           printfn "Draaaaw"
           playAgain ()
  


         //let x = Console.ReadLine()

(*let charcheck c =
     match c  with
     | 'A'| 'B'|'C'|'D'|'E'|'F'|'a'| 'b'|'c'|'d'|'e'|'f' -> true
     | _ -> false 
let b =6

//let placingcows input =

let msgPlacing = "Please enter the letter you want to place your cow at"
let msgMoving = "Please enter the letter you want to move your cow to"
let msgFlying = "Please enter the letter you want to fly your cow to"
let msgError = "Invalid output, please choose a different cell"   *)  
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let r = getinput ()
    match r with
       |'p'|'P' -> 
            clearboard()
            runGame CB blankBoard  //start with 24 cows, 12 for each. when this value reaches 0, go from placing to moving
       | _ -> rules ()
            
    //printBoard blankBoard
    
   // Console.Read()      

    0 // return an integer exit code