/*
Statement::
Little skater animation. He will push off the board every
once in ahwile.
The skater will attempt to dodge your mouse, up high and down low.
Unfortunately no fall/knockOff-effect if you touch him.

Citations::
(all png files created by me in PixilArt, total of 45)

*/
let interA,interB;
let col1,col2; 
let stationary = [];
let pushing = [];
let jump = [];
let duck = [];
let x1 = -80;
let imgIndex = 0;
let updateRate = 30;
let duckRate = 5;
let showingStationary = true;
let showingPushing = false;
let jumping = false;
let jumpIndex = 0;
let jumpRate = 8;
let shortBoost = false;
let slowDown = false;
let duckIndex = 0;
let ducking = false;


function setup() {
  createCanvas(600, 600);
  ///This code didn't work for some reason.
  for(let i = 0;i<=10;i++){
    let z = i+1;
    jump[i] = loadImage('images/jump-'+z+'.png');
  }
  for(let i = 0;i<4;i++){
    let z = i+1;
    stationary[i] = loadImage('images/stationary-'+z+'.png');
    pushing[i] = loadImage('images/pushing-'+z+'.png');
  }
  for(let i =0;i<31;i++){
    duck[i] = loadImage('images/duck-'+i+'.png');
  }
  
  colorMode(RGB);
  col1 = color(255,128,0);
  col2 = color(102,0,204);
  //this just gets a color between two color values.
  //lets me set top and bottom sunset colors, and not worry 
  //about making middle colors transition too eachother. 
  interA = lerpColor(col1,col2,0.7);
  interB = lerpColor(col1,col2,0.3);
}


function draw() {
  noStroke();
  background(220);
  fill(col2);
  rect(0,0,600,100);
  fill(interA);
  rect(0,100,600,100);
  fill(interB);
  rect(0,200,600,100);
  fill(col1);
  rect(0,300,600,100);
  fill('yellow');
  circle(300,400,350);
  fill('black');
  rect(0,400,600,200);
  
  // check which image array to display.
  // stationary, pushing, jumping, or ducking
  if(showingStationary === true){
    image(stationary[imgIndex],x1,255,200,200);
  }
  else{
    if(showingPushing === true){
      image(pushing[imgIndex],x1,255,200,200);
      if(imgIndex == 1){
        shortBoost = true;
      }
      if(imgIndex == 3){
        showingPushing = false;
        showingStationary = true;
        shortBoost = false;
        slowDown = true;
      }
    }
    else{
      if(jumping === true){
        image(jump[jumpIndex],x1,240,200,200);
        if(jumpIndex >= 10){
          jumping = false;
          jumpIndex = 0;
          showingStationary = true;
          showingPushing = false;
        }
      }
      else{
        if(ducking === true){
          image(duck[duckIndex],x1,255,200,200);
          if(duckIndex >= 30){
            ducking = false;
            jumping = false;
            showingStationary = true;
            showingPushing = false;
            duckIndex = 0;
          }
        }
      }
    }
  }
  
  //this moves the character
  x1 += 2;
  //decide whether he pushed off the ground.
  //if so, give little horizontal boost.
  if(shortBoost === true){
    x1 += 1;
  }
  else{ //just to slowdown the boost so transition is smooth.
    if(slowDown === true){
      x1 += 0.5;
    }
  }
  
  //these determine what speed to change images in arrays (framerate)
  if(frameCount % duckRate === 0){
    if(duckIndex < 30){
      duckIndex++;
    }
  }
  if(frameCount % updateRate === 0){
    imgIndex++;
    if(imgIndex >= 4){
      imgIndex = 0;
    }
  }
  if(frameCount % jumpRate === 0 && jumping === true){
    jumpIndex++;
  
  }
  
  //around 3 seconds, push off the board, get a little speed boost
  if(frameCount % 300 === 0 && jumping != true && ducking != true){
    showingPushing = true;
    showingStationary = false;
    imgIndex = 0;
  }
  if(x1 >= 600){
    x1 = -80;
    slowDown = false;
  }
  
  //check for mouse position. If in front of skater and low,
  //then ollie, if in front and high, then duck. 
  if(jumping == false && ducking == false){
  if((mouseX >= x1+75 && mouseX <= x1+200)&&(mouseY >= 368 && mouseY <= 400)){
    jumping = true;
    showingPushing = false;
    showingStationary = false;
  }else{
    if((mouseX >= x1+75 && mouseX <= x1+250)&&(mouseY >= 315 && mouseY <= 365)){
      ducking = true;
      jumping = false;
      showingPushing = false;
      showingStationary = false;
    }
  }
  }
  
}

