/*The purpose of the given shapes is to be able to draw mountains 
using only triangles, an oval, and a parallelogram. The colors given should be more than enough. Remember the first thing you draw is the first layer, so you should start by drawing the furthest thing away (e.g. sky, sun, mountains, lake, trees, would be a good order)

SHAPES::
Click on a shape to draw with it. Triangle and Quad are multi-click, shown in real time. Circle is a single click, for center of circle. Chosen shape will be highlighted. 

COLOR::
Choose your color! Just click it! Chosen color highlighted.

SIZE::
The size variable only effects the circle diameter. The circle diameter is 3,so 3*20 = 60 as starting circle size. Adjust from there. 

FILL::
Choose whether you want to make filled shapes, or noFill() shapes. 

TO ERASE::
to finish a drawing you must double click after placing the final point (circle's final points is first point, triangles is 3rd, rect is 4th).
If instead, you click ERASE after placing the final point, then click the screen again, it will erase. 
So when shape is in FINAL position, hit erase if you don't like it, and click on screen if you want to keep!
*/

var shape2Draw = 'triangle';
let shapeArray = [];
let shapeAppended = 'false';
let eraseButton = 'false';
let inBetweenShapes = 'false';
let bBlack,bGrey,bWhite,bBlue,bGreen,bRed,bYellow,bliBlue,bOrange,bCyan,bLime;
var chosenColor = 'grey';
var inUserLoop = 'false';
var bFill = 'true';
var totalSize = 20;
var addClickNum = 'false';
var drawing = 'false';
var clickNum = 0;
var x1 =0,y1 =0,x2 =0,y2 =0,x3 =0,y3 =0,x4 =0,y4 =0,x5 =0,y5 = 0;
function setup() {
  createCanvas(800, 800);
  
  bBlack = createButton('');
  bBlack.mouseClicked(fblack);
  bBlack.size(60, 30);
  bBlack.position(80, 750);
  bBlack.style('background-color', 'black');
  bBlack.style('border', 'none');
  
  bGrey = createButton('');
  bGrey.mouseClicked(fgrey);
  bGrey.size(60, 30);
  bGrey.position(170, 750);
  bGrey.style('background-color', 'grey');
  bGrey.style('border', '6px solid yellow');
  
  bWhite = createButton('');
  bWhite.mouseClicked(fwhite);
  bWhite.size(60, 30);
  bWhite.position(260, 750);
  bWhite.style('background-color', 'white');
  bWhite.style('border', 'none');
  
  bliBlue = createButton('');
  bliBlue.mouseClicked(fliBlue);
  bliBlue.size(20, 30);
  bliBlue.position(305, 750);
  bliBlue.style('background-color', 'lightblue');
  bliBlue.style('border', 'none');
  
  bBlue = createButton('');
  bBlue.mouseClicked(fblue);
  bBlue.size(60, 30);
  bBlue.position(350, 750);
  bBlue.style('background-color', 'blue');
  bBlue.style('border', 'none');
  
  bCyan = createButton('');
  bCyan.mouseClicked(fcyan);
  bCyan.size(20, 30);
  bCyan.position(395, 750);
  bCyan.style('background-color', 'cyan');
  bCyan.style('border', 'none');
  
  bGreen = createButton('');
  bGreen.mouseClicked(fgreen);
  bGreen.size(60, 30);
  bGreen.position(440, 750);
  bGreen.style('background-color', 'green');
  bGreen.style('border', 'none');
  
  bLime = createButton('');
  bLime.mouseClicked(flime);
  bLime.size(20, 30);
  bLime.position(485, 750);
  bLime.style('background-color', 'lime');
  bLime.style('border', 'none');
  
  bYellow = createButton('');
  bYellow.mouseClicked(fyellow);
  bYellow.size(60, 30);
  bYellow.position(530, 750);
  bYellow.style('background-color', 'yellow');
  bYellow.style('border', 'none');
  
  bOrange = createButton('');
  bOrange.mouseClicked(forange);
  bOrange.size(20, 30);
  bOrange.position(575, 750);
  bOrange.style('background-color', 'orange');
  bOrange.style('border', 'none');
  
  bRed = createButton('');
  bRed.mouseClicked(fred);
  bRed.size(60, 30);
  bRed.position(620, 750);
  bRed.style('background-color', 'red');
  bRed.style('border', 'none');
}

function getShape(){
  
  if(shape2Draw == 'triangle'){
    if(clickNum == 1){
      return triangle(x1,y1,mouseX,mouseY,mouseX,mouseY);
    }
    else{
      if(clickNum == 2){
        return triangle(x1,y1,x2,y2,mouseX,mouseY);
      }
      else{
        if(clickNum == 3){
          return triangle(x1,y1,x2,y2,x3,y3);
        }
        else{
          if(clickNum == 4){
            return triangle(x1,y1,x2,y2,x3,y3);
          }
          else{
            if(clickNum == 5){
              return triangle(x1,y1,x2,y2,x3,y3);
            }
          }
        }
      }
    }
  }
  else{
    if(shape2Draw == 'circle'){
      return circle(x1,y1,3*totalSize);
    }
    else{
      if(shape2Draw == 'rect'){
        if(clickNum == 1){
          return quad(x1,y1,mouseX,mouseY,mouseX,mouseY,mouseX,mouseY);
        }
        else{
          if(clickNum == 2){
            return quad(x1,y1,x2,y2,mouseX,mouseY,mouseX,mouseY);
          }
          else{
            if(clickNum == 3){
              return quad(x1,y1,x2,y2,x3,y3,mouseX,mouseY);
            }
            else{
              if(clickNum == 4){
                return quad(x1,y1,x2,y2,x3,y3,x4,y4);
              }
              else{
                if(clickNum == 5){
                  return quad(x1,y1,x2,y2,x3,y3,x4,y4);
                }
              }
            }
          }
        }
      }
    }
  }
}
/* SHAPE CLASS FOR ARRAY */

class shape{
  constructor(shapeDrawing,a1,b1,a2,b2,a3,b3,a4,b4,fillStatus,colur,size){
    this.shape = shapeDrawing;
    this.x1 = a1;
    this.x2 = a2;
    this.x3 = a3;
    this.x4 = a4;
    this.y1 = b1;
    this.y2 = b2;
    this.y3 = b3;
    this.y4 = b4;
    this.toFill = fillStatus;
    this.color = colur;
    this.size = size;
  }
  display(){
    if(this.shape == 'triangle'){
      if(this.toFill == 'true'){
        fill(this.color);
      }
      else{
        noFill();
      }
      triangle(this.x1,this.y1,this.x2,this.y2,this.x3,this.y3);
    }
    else{
      if(this.shape == 'circle'){
        if(this.toFill == 'true'){
          fill(this.color);
        }
        else{
          noFill();
        }
        circle(this.x1,this.y1,3*this.size);
        }
      else{
        if(this.shape == 'rect'){
          if(this.toFill == 'true'){
          fill(this.color);
        }
        else{
          noFill();
        }
        quad(this.x1,this.y1,this.x2,this.y2,this.x3,this.y3,this.x4,this.y4);
          }
        }
      }
  }
}
/** DRAW FUNCTION **/

function draw() {
  background(220);
  
  for(let j=0;j<shapeArray.length;j++){
    shapeArray[j].display();
  }
  if(drawing == 'true'){
    if(bFill == 'true'){
      fill(chosenColor);
    }
    else{
      noFill();
    }
    
    //code to draw triangle in real time
    if(shape2Draw == 'triangle'){
      getShape();
      if(clickNum == 4 && shapeAppended == 'false'){
        if(eraseButton == 'false'){
        shapeArray.push(new shape('triangle',x1,y1,x2,y2,x3,y3,x4,y4,bFill,chosenColor,totalSize));
        }
        shapeAppended = 'true';
        drawing = 'false';
        inBetweenShapes = 'true';
      }
    }
    //code to draw circle in real time
    else{
      if(shape2Draw == 'circle'){
        getShape();
        if(clickNum == 2 && shapeAppended == 'false'){
        if(eraseButton == 'false'){
        shapeArray.push(new shape('circle',x1,y1,x2,y2,x3,y3,x4,y4,bFill,chosenColor,totalSize));
        }
        shapeAppended = 'true';
        drawing = 'false';
        inBetweenShapes = 'true';
      }
      }
      else{
        if(shape2Draw == 'rect'){
          getShape();
          if(clickNum == 5 && shapeAppended == 'false'){
            if(eraseButton == 'false'){
              shapeArray.push(new shape('rect',x1,y1,x2,y2,x3,y3,x4,y4,bFill,chosenColor,totalSize));
              }
            shapeAppended = 'true';
            drawing = 'false';
            inBetweenShapes = 'true';
      }
      }
      }
    }
  }
  print("Shape Array Length = "+shapeArray.length);
  fill("silver");
  rect(-10,650,820,150);
  /*shape selectors*/
  fill("white");
  if(shape2Draw == "triangle"){
    fill("lightgrey");
  }
  rect(120,700,50,40);
  noFill();
  triangle(127,733,142,707,163,733);
  fill("white");
  if(shape2Draw == "circle"){
    fill("lightgrey");
  }
  rect(210,700,50,40);
  noFill();
  circle(235,720,30);
  fill("white");
  if(shape2Draw == "rect"){
    fill("lightgrey");
  }
  rect(300,700,50,40);
  noFill();
  rect(305,710,40,20);
  /*shape tools*/
  fill("white");
  rect(390,700,50,40);
  fill("black");
  textSize(18);
  if(bFill == 'true'){
    text("fill()",400,725);
  }
  else{
    textSize(14);
    text("noFill()",395,725);
  }
  fill("white");
  rect(480,700,70,40);
  fill("black");
  textSize(18);
  text("< size >",485,735);
  text(totalSize,505,717);
  
  fill("white");
  rect(580,700,80,40);
  fill("black");
  textSize(18);
  text("ERASE",590,725);
}

function increaseSize(){
  totalSize += 1;
}
function decreaseSize(){
  if(totalSize >= 2){
    totalSize -= 1;
  }
}
function fillFunc(){
  
}
function circleFunc(){
  shape2Draw = 'circle';
}
function triangleFunc(){
  shape2Draw = 'triangle';
}
function rectFunc(){
  shape2Draw = 'rect';
}

function mousePressed(){
  //print(mouseX,mouseY);
  if((mouseX <= 800 && mouseX >= 0)&&(mouseY <=650 && mouseY >= 0)){
    print("draw state "+drawing);
    if(drawing == 'true'){
      clickNum ++;
      //check to set x and y coordinates
      if(clickNum == 2){
            x2 = mouseX;
            y2 = mouseY;
          }
          else{
            if(clickNum == 3){
              x3 = mouseX;
              y3 = mouseY;
            }
            else{
              if(clickNum == 4){
                x4 = mouseX;
                y4 = mouseY;
              }
              else{
                if(clickNum == 5){
                  x5 = mouseX;
                  y5 = mouseY;
                }
              }
            }
          }
    }
    else{
    if(drawing == 'false'){
      if(inBetweenShapes == 'true'){
        print('in between shapes');
        inBetweenShapes = 'false';
        drawing = 'false';
      }
      else{
        eraseButton = 'false';
        clickNum = 1;
        drawing = 'true';
        x1 = mouseX;
        y1 = mouseY;
        print('not in between shapes, drawing = true');
        shapeAppended = 'false';
      }
      }
    }
  }
  else{
  if((mouseX <= 440 && mouseX >= 390)&&(mouseY <= 740 && mouseY >= 700)){
    print("HIT FILL BUTTON");
    fillFunc();
    if(bFill == 'true'){
      bFill = 'false';
    }
    else{
      bFill = 'true';
    }
  }
  else{
    if((mouseX <= 546 && mouseX >= 533)&&(mouseY <= 735 && mouseY >= 724)){
    print("increase size");
    increaseSize();
    }
    else{
      if((mouseX <= 500 && mouseX >= 483)&&(mouseY <= 736 && mouseY >= 720)){
    print("decrease size");
    decreaseSize();
    }
    else{
      if((mouseX <= 168 && mouseX >= 120)&&(mouseY <= 740 && mouseY >= 700)){
    print("HIT triangle button");
    triangleFunc();
    }
    else{
      if((mouseX <= 258 && mouseX >= 211)&&(mouseY <= 740 && mouseY >= 700)){
    print("HIT Circle button");
    circleFunc();
    }
    else{
      if((mouseX <= 348 && mouseX >= 301)&&(mouseY <= 740 && mouseY >= 700)){
    rectFunc();
    print("HIT rect button");
    }
    else{
      if((mouseX <= 660 && mouseX >= 580)&&(mouseY <= 740 && mouseY >= 700)){
    //erase
    print("HIT ERASE button");
    if(drawing == 'true'){
      eraseButton = 'true';
    }
    }
    }
    }
  }
  }
}
  }
  }
}

function fblack(){
  print('black button');
  chosenColor = 'black';
  
  bBlack.style('border', '6px solid yellow');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bOrange.style('border','none');
}
function fgrey(){
  print('grey button');
  chosenColor = 'grey';
  
  bGrey.style('border', '6px solid yellow');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bOrange.style('border','none');
  bBlack.style('border','none');
}
function fwhite(){
  print('white button');
  chosenColor = 'white';
  
  bWhite.style('border', '6px solid yellow');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bOrange.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
}
function fliBlue(){
  print('liBlue button');
  chosenColor = 'lightblue';
  
  bliBlue.style('border', '6px solid yellow');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bOrange.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
}
function fblue(){
  print('blue button');
  chosenColor = 'blue';
  
  bBlue.style('border', '6px solid yellow');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bOrange.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
}
function fcyan(){
  print('cyan button');
  chosenColor = 'cyan';
  
  bCyan.style('border', '6px solid yellow');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bOrange.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
}
function fgreen(){
  print('green button');
  chosenColor = 'green';
  
  bGreen.style('border', '6px solid yellow');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bOrange.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
}
function flime(){
  chosenColor = 'lime';
  
  bLime.style('border', '6px solid black');
  bRed.style('border','none');
  bYellow.style('border','none');
  bOrange.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
  print('lime button');
}
function fyellow(){
  chosenColor = 'yellow';
  
  bYellow.style('border', '6px solid black');
  bRed.style('border','none');
  bOrange.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
  print('yellow button');
}
function forange(){
  chosenColor = 'orange';
  
  bOrange.style('border', '6px solid black');
  bRed.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
  print('orange button');
}
function fred(){
  chosenColor = 'red';
  
  bRed.style('border', '6px solid yellow');
  bOrange.style('border','none');
  bYellow.style('border','none');
  bLime.style('border','none');
  bGreen.style('border','none');
  bCyan.style('border','none');
  bBlue.style('border','none');
  bliBlue.style('border','none');
  bWhite.style('border','none');
  bGrey.style('border','none');
  bBlack.style('border','none');
  print('red button');
}
  

/* CODE THAT MESSED ME UP!
function userDraw(){
  inUserLoop = 'true';
  if(bFill == 'true'){
      fill(chosenColor);
    }
  else{
    noFill();
  }  
    print("HERE IN CODE ");
    if(shape2Draw == 'triangle'){
       print(x1,y1,x2,y2,x3,y3,x4,y4);
      print("GOING INTO WHILE");
      while(clickNum != 4){
        if(clickNum == 1){
          print(x1,y1,x2,y2,x3,y3,x4,y4,x5,y5);
        }
        else{
          if(clickNum == 2){
            print(x1,y1,x2,y2,x3,y3,x4,y4,x5,y5);
          }
          else{
            if(clickNum == 3){
              print(x1,y1,x2,y2,x3,y3,x4,y4,x5,y5);
            }
            else{
              if(clickNum == 4){
                print(x1,y1,x2,y2,x3,y3,x4,y4,x5,y5);
              }
              else{
                if(clickNum == 5){
                  print(x1,y1,x2,y2,x3,y3,x4,y4,x5,y5);
                }
              }
            }
          }
        }
        
       //check to see if we should ++ clickNum
      if(addClickNum == 'true'){
      clickNum++;
      addClickNum = 'false';
        }
      }
    }
  inUserLoop = 'false';
  x1 = 0;
  x2 = 0;
  x3 = 0;
  x4 = 0;
  x5 = 0;
  y1 = 0;
  y2 = 0;
  y3 = 0;
  y4 = 0;
  y5 = 0;
}*/