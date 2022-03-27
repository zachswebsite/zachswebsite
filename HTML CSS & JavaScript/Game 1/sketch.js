/*

Space bar to jump, shift to crouch. 
Make it to the end!!! - Trust me it's possible... I've done it... Once...

I decided to expand off our user interactivity object. Originally the skater would jump over or duck under your mouse to avoid you. I changed that to more of a runner game.

Trying to get 'CTRL' to throw up checkpoint flags. Doesn't work yet.
Also trying to get a 'restart' button so you don't have to refresh the game each loss.

I created the art myself using PixilArt.
*/
var player;
var obstacles = [];
var rects = [];
var triangles = [];
var trianglesUp = [];
var jump, stationary, duck,pushing,still,tri;
let gravity = 0.15
let hop = -3;
let rightVelocity = 2;
let landed = false;
let crouching = false;
var dead = false;
//array is for triangles rotated by 180 degrees
const array = [3,4,5,6,7,8,9,10,23,24,25,26,27,28,29,30,31];
const rot90 = [];
const rot270 = [];
let checkpointX = 200;
let checkpointY = 400;
let col1,col2, interA,interB;
let x1,y1;


function preload(){ 
  
  jump = loadAnimation("images/jump-1.png","images/jump-2.png","images/jump-3.png","images/jump-4.png","images/jump-5.png","images/jump-6.png","images/jump-7.png","images/jump-8.png","images/jump-9.png","images/jump-10.png","images/jump-11.png");
  stationary = loadAnimation("images/stationary-1.png","images/stationary-2.png","images/stationary-3.png","images/stationary-4.png");
  duck = loadAnimation("images/duck-0.png","images/duck-1.png","images/duck-2.png","images/duck-3.png","images/duck-4.png","images/duck-5.png","images/duck-6.png","images/duck-7.png","images/duck-7.png","images/duck-23.png","images/duck-24.png","images/duck-25.png","images/duck-26.png","images/duck-27.png","images/duck-28.png","images/duck-29.png","images/duck-30.png");
  pushing = loadAnimation("images/pushing-1.png","images/pushing-2.png","images/pushing-3.png","images/pushing-4.png");
  still = loadImage("images/stationary-2.png");
  tri = loadImage("images/triangle3.png");
}


function setup() {
  createCanvas(600, 600);
  
  player = createSprite(200,400);
  player.velocity.x = rightVelocity;
  //player.debug = true;
  //create animations
  
  //setup colors
  colorMode(RGB);
  col1 = color(255,128,0);
  col2 = color(102,0,204);
  //this just gets a color between two color values.
  //lets me set top and bottom sunset colors, and not worry 
  //about making middle colors transition too eachother. 
  interA = lerpColor(col1,col2,0.7);
  interB = lerpColor(col1,col2,0.3);


  
  //create sprite and add animations to it
  player.addAnimation("jump",jump);
  player.addAnimation("stationary",stationary);
  player.addAnimation("duck",duck);
  player.addAnimation("pushing",pushing);
  player.addImage("still",still);
  player.changeAnimation("still");
  player.setCollider('rectangle',-8, 0, 20, 45);
  //change height of collider when ducking, so last value
  
  //create obstacles
  obstacles.push(createSprite(0,500,16000,100));
  obstacles.push(createSprite(950,380,150,20));
  obstacles.push(createSprite(1300,435,25,30));
  obstacles.push(createSprite(1405,435,25,30));
  
  obstacles.push(createSprite(2300,440,20,20));
  obstacles.push(createSprite(2360,410,20,20));
  obstacles.push(createSprite(2420,380,20,20));
  obstacles.push(createSprite(2480,350,20,20));
  obstacles.push(createSprite(2540,320,20,20));
  obstacles.push(createSprite(2600,290,20,20));
  obstacles.push(createSprite(2660,260,20,20)); //#12
  obstacles.push(createSprite(2700,290,20,20));
  obstacles.push(createSprite(2740,320,20,20));
  obstacles.push(createSprite(2740,230,20,20)); //#hit  #15
  obstacles.push(createSprite(2800,290,20,20));
  obstacles.push(createSprite(2840,260,20,20));
  obstacles.push(createSprite(2880,290,20,20));
  obstacles.push(createSprite(3000,320,200,20));
  obstacles.push(createSprite(3035,250,200,20)); //#20, top trap
  obstacles.push(createSprite(3130,340,20,20));
  obstacles.push(createSprite(3170,370,20,20));
  obstacles.push(createSprite(3210,400,20,20));
  obstacles.push(createSprite(3250,430,20,20));
  obstacles.push(createSprite(4190,440,25,25));
  obstacles.push(createSprite(4270,410,25,25));
  obstacles.push(createSprite(4310,420,25,25));

  obstacles.push(createSprite(5300,440,20,20));
  obstacles.push(createSprite(5360,410,20,20));
  obstacles.push(createSprite(5420,380,20,20));
  obstacles.push(createSprite(5460,410,20,20));
  obstacles.push(createSprite(5560,410,20,20));
  obstacles.push(createSprite(5620,380,20,20));
  obstacles.push(createSprite(5660,410,20,20));
  obstacles.push(createSprite(5760,410,20,20));
  obstacles.push(createSprite(5820,380,20,20));
  obstacles.push(createSprite(5880,350,20,20));
  obstacles.push(createSprite(5920,380,20,20));
  obstacles.push(createSprite(5960,410,20,20));
  obstacles.push(createSprite(6000,440,20,20));



  
  for(var l =0;l<obstacles.length;l++){
    obstacles[l].immovable = true;
    obstacles[l].shapeColor = "grey";
    //obstacles[l].debug = true;
  }
  
  
  triangles.push(createSprite(500,440));
  trianglesUp.push(createSprite(500,440));
  triangles.push(createSprite(725,440));
  trianglesUp.push(createSprite(725,440))
  triangles.push(createSprite(740,440));
  trianglesUp.push(createSprite(740,440));
  triangles.push(createSprite(950,400));
  trianglesUp.push(createSprite(950,400));
  triangles.push(createSprite(930,400));
  triangles.push(createSprite(910,400));
  triangles.push(createSprite(890,400));
  triangles.push(createSprite(950,400));
  triangles.push(createSprite(970,400));
  triangles.push(createSprite(990,400));
  triangles.push(createSprite(1010,400));
  trianglesUp.push(createSprite(930,400));
  trianglesUp.push(createSprite(910,400));
  trianglesUp.push(createSprite(890,400));
  trianglesUp.push(createSprite(950,400));
  trianglesUp.push(createSprite(970,400));
  trianglesUp.push(createSprite(990,400));//#10
  trianglesUp.push(createSprite(1010,400));
  triangles.push(createSprite(1320,440));
  trianglesUp.push(createSprite(1320,440));
  triangles.push(createSprite(1340,440));
  trianglesUp.push(createSprite(1340,440));
  triangles.push(createSprite(1360,440));
  trianglesUp.push(createSprite(1360,440));
  triangles.push(createSprite(1380,440));
  trianglesUp.push(createSprite(1380,440));//#15
  
  triangles.push(createSprite(1600,440));
  trianglesUp.push(createSprite(1600,440));
  triangles.push(createSprite(1700,440));
  trianglesUp.push(createSprite(1700,440));
  triangles.push(createSprite(1715,440));
  trianglesUp.push(createSprite(1715,440));
  triangles.push(createSprite(1800,440));
  trianglesUp.push(createSprite(1800,440));
  triangles.push(createSprite(1815,440));
  trianglesUp.push(createSprite(1815,440));//#20
  triangles.push(createSprite(1880,440));
  trianglesUp.push(createSprite(1880,440));
  triangles.push(createSprite(1895,440));
  trianglesUp.push(createSprite(1895,440));
  
  triangles.push(createSprite(2740,210)); //#hit block
  trianglesUp.push(createSprite(2740,210)); //#hit block

  
  triangles.push(createSprite(3035,270)); //#row of triangles
  triangles.push(createSprite(3015,270));
  triangles.push(createSprite(3000,270));
  triangles.push(createSprite(3055,270));
  triangles.push(createSprite(3075,270));
  triangles.push(createSprite(3095,270));
  
  triangles.push(createSprite(2980,270));
  triangles.push(createSprite(2960,270));
  triangles.push(createSprite(3115,270));

  triangles.push(createSprite(3000,230));
  triangles.push(createSprite(3020,230));
  triangles.push(createSprite(3040,230));
  triangles.push(createSprite(3060,230));



  trianglesUp.push(createSprite(3015,270));
  trianglesUp.push(createSprite(3000,270));
  trianglesUp.push(createSprite(3055,270));
  trianglesUp.push(createSprite(3075,270));
  trianglesUp.push(createSprite(3095,270));
  trianglesUp.push(createSprite(3035,270));
  trianglesUp.push(createSprite(2980,270));
  trianglesUp.push(createSprite(2960,270));
  trianglesUp.push(createSprite(3115,270));
  trianglesUp.push(createSprite(3000,230));
  trianglesUp.push(createSprite(3020,230));
  trianglesUp.push(createSprite(3040,230));
  trianglesUp.push(createSprite(3060,230));

  //triangles after first landing
  triangles.push(createSprite(3800,440));
  trianglesUp.push(createSprite(3800,440));
  
  triangles.push(createSprite(3900,440));
  trianglesUp.push(createSprite(3900,440));
  triangles.push(createSprite(3915,440));
  trianglesUp.push(createSprite(3915,440));
  
  triangles.push(createSprite(3985,440));
  trianglesUp.push(createSprite(3985,440));
  triangles.push(createSprite(4000,440));
  trianglesUp.push(createSprite(4000,440));
  
  triangles.push(createSprite(4150,440));
  trianglesUp.push(createSprite(4150,440));
  triangles.push(createSprite(4165,440));
  trianglesUp.push(createSprite(4165,440));
  
  triangles.push(createSprite(4215,440));
  trianglesUp.push(createSprite(4215,440));
  triangles.push(createSprite(4235,440));
  trianglesUp.push(createSprite(4235,440));
  
  
  triangles.push(createSprite(4330,440));
  trianglesUp.push(createSprite(4330,440));
  triangles.push(createSprite(4350,440));
  trianglesUp.push(createSprite(4350,440));
  triangles.push(createSprite(4370,440));
  trianglesUp.push(createSprite(4370,440));
  triangles.push(createSprite(4390,440));
  trianglesUp.push(createSprite(4390,440));
  
  triangles.push(createSprite(4600,440));
  trianglesUp.push(createSprite(4600,440));
  
  triangles.push(createSprite(4685,440));
  trianglesUp.push(createSprite(4685,440));
  triangles.push(createSprite(4700,440));
  trianglesUp.push(createSprite(4700,440));
  
  triangles.push(createSprite(4785,440));
  trianglesUp.push(createSprite(4785,440));
  triangles.push(createSprite(4800,440));
  trianglesUp.push(createSprite(4800,440));
  
  triangles.push(createSprite(4875,440));
  trianglesUp.push(createSprite(4875,440));
  triangles.push(createSprite(4890,440));
  trianglesUp.push(createSprite(4890,440));
  
  triangles.push(createSprite(5520,440));
  trianglesUp.push(createSprite(5520,440));
  triangles.push(createSprite(5540,440));
  trianglesUp.push(createSprite(5540,440));
  
  triangles.push(createSprite(5740,440));
  trianglesUp.push(createSprite(5740,440));
  triangles.push(createSprite(5760,440));
  trianglesUp.push(createSprite(5760,440));
  
  triangles.push(createSprite(6020,440));
  trianglesUp.push(createSprite(6020,440));
  triangles.push(createSprite(6035,440));
  trianglesUp.push(createSprite(6035,440));
  triangles.push(createSprite(6050,440));
  trianglesUp.push(createSprite(6050,440));
  triangles.push(createSprite(6065,440));
  trianglesUp.push(createSprite(6065,440));
  
  triangles.push(createSprite(3100,440));
  trianglesUp.push(createSprite(3100,440));
  triangles.push(createSprite(3120,440));
  trianglesUp.push(createSprite(3120,440));
  triangles.push(createSprite(3140,440));
  trianglesUp.push(createSprite(3140,440));
  triangles.push(createSprite(3160,440));
  trianglesUp.push(createSprite(3160,440));
  
  //loop for creating triangles with horizontal collision box
  for(var i=0;i<triangles.length;i++){
    if(array.includes(i)){
      triangles[i].rotation = 180;
      triangles[i].setCollider('rectangle',0,-10,15,3);
    }else if(rot270.includes(i)){
      triangles[i].rotation = 270;
      triangles[i].setCollider('rectangle',10,0,15,2);
    }else if(rot90.includes(i)){
      triangles[i].rotation = 90;
    }else{
      triangles[i].setCollider('rectangle',0,10,15,2);
    }
    triangles[i].addImage(tri);
    //triangles[i].debug = true;
    triangles[i].immovable = true;
  }
  
  //loop for creating triangles with vertical collision box
  for(var z=0;z<trianglesUp.length;z++){
    if(array.includes(z)){
      trianglesUp[z].rotation = 180
    }
    trianglesUp[z].addImage(tri);
    //trianglesUp[z].debug = true;
    trianglesUp[z].immovable = true;
    trianglesUp[z].setCollider('rectangle',0,0,2,17);
  }
  
}

function draw() {
  background(220);
  x1 = map(player.position.x,0,6000,0,250);
  y1 = map(player.position.x,0,6000,0,150);
  //parallax positions
  //move camera with player
  camera.position.x = player.position.x;
  fill(col2);
  rect(camera.position.x-window.width/2,0,600,120);
  fill(interA);
  rect(camera.position.x-window.width/2,120,600,120);
  fill(interB);
  rect(camera.position.x-window.width/2,240,600,120);
  fill(col1);
  rect(camera.position.x-window.width/2,360,600,120);
  fill('yellow');
  circle((camera.position.x)-x1,400+y1,350);
  fill('black');
  rect(camera.position.x-window.width/2,500,600,100);
  //check for collision with every sprite
  for(let i=0;i<obstacles.length;i++){
    if(player.collide(obstacles[i])){
      landed = true;
    }
  }
  
  for(let i=0;i<triangles.length;i++){
    if(player.collide(triangles[i])){
      dead = true;
    }else if(player.collide(trianglesUp[i])){
      dead = true;
    }
  }
  
  
  if(dead == true){
    //console.log("dead");
  }
  if(dead != true){
  player.velocity.x = rightVelocity;
  }else{
    player.velocity.x = 0;
  }
  if(player.velocity.y < 3){
    //if velocity.y gets above 45, the player will fall through objects
    player.velocity.y += gravity;
  }
  if(landed == true && crouching != true){
    player.changeAnimation("still");
  }
  if(keyIsDown(SHIFT)){
    if(landed == true){
      crouching = true;
      player.setCollider('rectangle',-8, 10, 20, 20);
      player.changeAnimation("duck");
      if(player.animation.getFrame() == 7){
        player.animation.stop();
      }
    }
  }else{
    player.animation.play();
    if(player.animation.getFrame() == player.animation.getLastFrame()){
      crouching = false;
      player.setCollider('rectangle',-8, 0, 20, 45);
    }
  }
  drawSprites();
}

function keyPressed() {
  if (key == ' ') {
    if(landed == true){
      player.velocity.y = hop;
      landed = false;
      player.changeAnimation("jump");
    }
    //player.velocity.y = hop;
    //console.log("SPACE");
  }else if(keyCode === CONTROL){
    checkpointX = player.position.x;
    checkpointY = player.position.y;
    console.log(player.position.x);
    
  }
    
}