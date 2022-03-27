/*
Mouse movement scares the cute fellas. Keep the mouse still to keep them moving again. 

After clicking on the screen, press any key to turn on the lights. 

Be careful about clicking on the screen when the lights are on!

Feel free to change parasite count # below 
*/

let parasiteCount = 36;



// declare variables for object instances
let parasiteImages = [];
let parasites = [];
let xm;
let ym;
let stationary = false;
let lightsOff = true;
let fr = 170;
let attack = false;
let jumpAttack = false;
let jump = false;
let gameOver = false;

function setup() {
  createCanvas(600, 600);
  parasiteImages[0] = loadImage('stationary.png');
  parasiteImages[1] = loadImage('squish.png');
  parasiteImages[2] = loadImage('jump.png');
  
  ym = mouseY;
  xm = mouseX;
  
  //create 'x' parasites
  for(let i=1;i<=sqrt(parasiteCount);i++){
    for(let j=1;j<=sqrt(parasiteCount);j++){
      parasites.push(new Parasite(i*(550/sqrt(parasiteCount)),j*(550/sqrt(parasiteCount))));
    }
  }
}

//turn lights off if key pressed
function keyPressed(){
  if(lightsOff === true){
    lightsOff = false;
  }else{
    if(attack != true){
      lightsOff = true;
    }
  }
}

//get xm ym mouse click values, and if lights are on, attack!
function mouseClicked(){
  if((mouseX > 0 && mouseX < width)&&(mouseY>0 && mouseY <height)){
    xm = mouseX;
    ym = mouseY;
    if(lightsOff === false){
      attack = true;
    }
  }
}

//if moving mouse within window, freeze the critters
function mouseMoved(){
  if((mouseX > 0 && mouseX < width)&&(mouseY>0 && mouseY <height)){
    stationary = true;
  }else{
    stationary = false;
  }
}


function draw() {
  if(lightsOff === true){
    background('black');
  }else{
    background(220);
  }
  
  for(let i=0;i<parasites.length;i++){
    parasites[i].display();
    if(frameCount % 4 === 0){
    for(let j=0;j<parasites.length;j++){
      if(i != j && parasites[i].intersects(parasites[j])){
        //intersecting, call method to go in different direction?
        if(random(0.0,1.0) < 0.5){
          parasites[i].bounce();
        }else{
          parasites[j].bounce();
        }
      }
    }
  }
}
  if(frameCount % fr === 0){
    stationary = false;
  }
  if(gameOver === true){
    background('black');
  }
  //THESE ARE ALL WEBGL ONLY
  //ambientLight(50);
  //spotLight(0, 250, 0, xm, ym, 100, 0, 0, -1, Math.PI / 16);
  //directionalLight(250, 250, 250, -dirX, -dirY, -1);
  //lights();
  
}

/*
only interesting things here are I create a radius value for each 
creature, for collision detection, and I create constrain values xc,xy,
that are mapped to mouse x and mouse y values, that keep the eyes from moving more than 10 pixels to the right and left. This is what causes eyes to follow mouse.
*/
class Parasite {
  xc;
  yc;
  r;
  rx;
  ry;
  intersects;
  bounce;
  squishFR1;
  squishFR2;
  imageToDisplay;

  // called with "new Creature" in setup
  // instantiates an object
  constructor(inX, inY) {
    this.x = inX;
    this.y = inY;
    //radius of object, for collision
    this.r = 30;
    this.imageToDisplay = 0;
    //framerate
    this.squishFR1 = round(random(40,90));
    this.squishFR2 = round(random(200,700));
    //direction, random
    this.rx = random(-0.3,0.3);
    this.ry = random(-0.3,0.3);
    //eye constraint
    this.xc = constrain(mouseX,inX-5,inX+5);
    this.yc = constrain(mouseY,inY-6,inY+6);
    
    //function for intersecting objects
    this.intersects = function(other){
      let d = dist(this.x,this.y,other.x,other.y);
      if(d < this.r + other.r){
        return true;
      }else{
        return false;
      }
    }
    
    //function for bouncing off walls
    this.bounce = function(){
      this.rx = -this.rx;
      this.ry = -this.ry;
    }
  }

  display() {
    //this moves creatures
    if(stationary == false && attack == false){
      this.x = this.x+this.rx;
      this.y = this.y+this.ry;
    }
    if(jump === true){
      this.rx = (xm - this.x)/random(16,20);
      this.ry = (ym - this.y)/random(16,20);
      this.x = this.x+this.rx;
      this.y = this.y+this.ry;
      if(dist(this.x,this.y,xm,ym) <= 8){
        gameOver = true;
      }
      }
    imageMode(CENTER);
    
    //decide which image to load
    if(lightsOff === false){
      
        //cycle through moving and non moving image
      image(parasiteImages[this.imageToDisplay],this.x,this.y,100,100);
      
      if(frameCount % this.squishFR1 === 0 && attack == false){
           this.imageToDisplay = 0;
        }
      
      if(frameCount % this.squishFR2 === 0 && attack == false && stationary == false){
          this.imageToDisplay = 1;
        }
    }
    //constrain eye movement
    this.xc = constrain(mouseX,this.x-8,this.x);
    this.yc = constrain(mouseY,this.y-12,this.y-4);
    
    //wall collision detection
    if(this.x-30 < 0 || this.x+30 > width){
      this.rx = -this.rx;
    }
    if(this.y-35 < 0 || this.y+15 > height){
      this.ry = -this.ry;
    }
    
    push();
    
    // translate to this.x, this.y so that 0,0
    // is the center of the creature while we draw
    // (then we don't have to do math using this.x
    // and this.y, though that is also a valid approach,
    // if it is more comfortable and familiar to you)
    
    translate(this.x, this.y);

  
    stroke(3);
    if(attack == false){
    //white of eye
    fill('white');
    beginShape();
    vertex(-20,-8); 
    bezierVertex(-4,-20,-4,-20,12,-8);
    bezierVertex(-4,4,-4,4,-20,-8);
    endShape();
    }
    pop();
    
    //eyeball
    if(attack == false){
    fill('red');
    circle(this.xc,this.yc,15);
    fill('black');
    circle(this.xc,this.yc,7);
    fill('white');
    circle(this.xc-1.5,this.yc-1.5,2.5);
    
    
    //grey around eye, which allows eye to appear cutoff
    push();
    translate(this.x,this.y);
    noStroke();
    if(lightsOff === true){
      fill('black');
    }else{
      fill('#404040');
    }
    beginShape();
    vertex(-26,-8); 
    bezierVertex(-4,-28,-4,-28,18,-8);
    bezierVertex(-4,12,-4,12,-26,-8);
    beginContour();
    vertex(-20,-8); 
    bezierVertex(-4,4,-4,4,12,-8);
    bezierVertex(-4,-20,-4,-20,-20,-8);
    endContour();
    endShape(CLOSE);
    pop();
    }else{ //attack mode is true, so draw line, then eye
      if(jumpAttack != true){
      push();
      translate(this.x,this.y);
      stroke(12);
      line(-20,-6,12,-6);
      pop();
      if(frameCount % fr === 0){
        jumpAttack = true;
        }
      }else{
        push();
        translate(this.x,this.y);
        stroke(12);
        fill('black');
        circle(-3,-8,20);
        fill('white');
        triangle(-5,-18,-1,-18,-3,-8);
        
        triangle(-9,-16,-11,-13,-4,-8);
        triangle(3,-16,5,-13,-2,-8);
        
        triangle(-9,0,-11,-2,-4,-8);
        triangle(3,0,5,-2,-2,-8);
        
        triangle(-13,-10,-13,-6,-3,-8);
        triangle(7,-10,7,-6,-3,-8);
        
        triangle(-5,2,-1,2,-3,-8);
        pop();
        if(frameCount % fr === 0){
          jump = true;
          this.imageToDisplay = 2;
        }
      }
    }
    

    //---------------------------------------------
    
  }
  
  
}