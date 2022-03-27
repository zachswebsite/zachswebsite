function Player() {
  //position
  this.y = height/5;
  this.x = 64;
  
  //collision box positions
  this.topY = this.y;
  this.botY = this.y+40;
  this.leftX = this.x;
  this.rightX = this.x+20;
  
  //skateboard collision box
  this.boardX = this.x+100;
  this.boardY = this.y+50;
  
  //gravity, lift, speed
  this.gravity = 0.5;
  this.lift = -17;
  this.velocity = 0;
  this.landed = false;
  
  //setting up image arrays, bools, and frameRates
  this.stationary = [];
  this.pushing = [];
  this.jump = [];
  this.duck = [];
  this.imgIndex = 0;
  this.updateRate = 30;
  this.duckRate = 5;
  this.showingStationary = true;
  this.showingPushing = false;
  this.jumping = false;
  this.jumpIndex = 0;
  this.jumpRate = 8;
  this.shortBoost = false;
  this.slowDown = false;
  this.duckIndex = 0;
  this.ducking = false;
  
  for(let i = 0;i<11;i++){
    let z = i+1;
    this.jump[i] = loadImage('images/jump-'+z+'.png');
  }
  for(let i = 0;i<4;i++){
    let z = i+1;
    this.stationary[i] = loadImage('images/stationary-'+z+'.png');
    this.pushing[i] = loadImage('images/pushing-'+z+'.png');
  }
  for(let i =0;i<31;i++){
    this.duck[i] = loadImage('images/duck-'+i+'.png');
  }
  

  this.show = function() {
    if(this.showingStationary === true){
    image(this.stationary[this.imgIndex],this.x,this.y,200,200);
    if(frameCount % this.updateRate === 0){
      this.imgIndex++;
    }
      if(this.imgIndex >= 4){
        this.imgIndex = 0;
      }
  }
  else{
    if(this.showingPushing === true){
      image(this.pushing[this.imgIndex],this.x,this.y,200,200);
      if(this.imgIndex == 1){
        this.shortBoost = true;
      }
      if(this.imgIndex == 3){
        this.showingPushing = false;
        this.showingStationary = true;
        this.shortBoost = false;
        this.slowDown = true;
      }
      if(frameCount % this.updateRate === 0){
      this.imgIndex++;
      }
      if(this.imgIndex >= 4){
        this.imgIndex = 0;
      }
    }
    else{
      if(this.jumping === true){
        print(this.jumpIndex);
        image(this.jump[this.jumpIndex],this.x,this.y,200,200);
        if(frameCount % this.jumpRate === 0){
          this.jumpIndex++;
        }
        if(this.jumpIndex >= 10){
          this.jumping = false;
          this.jumpIndex = 0;
          this.showingStationary = true;
          this.showingPushing = false;
        }
      }
      else{
        if(this.ducking === true){
          image(this.duck[this.duckIndex],x1,255,200,200);
          if(frameCount % this.duckRate === 0){
            this.duckIndex ++;
          }
          if(this.duckIndex >= 30){
            this.ducking = false;
            this.jumping = false;
            this.showingStationary = true;
            this.showingPushing = false;
            this.duckIndex = 0;
          }
        }
      }
    }
  }
  }
  
  this.stationaryFunc = function(){
    this.showingStationary = true;
    this.showingPushing = false;
    this.ducking = false;
    this.jumping = false;
    this.imgIndex = 0;
    this.velocity = 0;
  }
  
  this.jumpFunc = function(){
    this.showingStationary = false;
    this.showingPushing = false;
    this.ducking = false;
    this.jumping = true;
    this.jumpIndex = 0;
    this.velocity += this.lift;
  }

  this.up = function() {
    this.velocity += this.lift;
  }

  this.update = function() {
    //collision box positions
  this.topY = this.y;
  this.botY = this.y+150;
  this.leftX = this.x;
  this.rightX = this.x+10;
  
  //skateboard collision box
  this.boardX = this.x+100;
  this.boardY = this.y+210;
    //if not landed (on platform) add gravity to y
    if(this.landed == false){
      this.velocity += this.gravity;
      this.y += this.velocity;    
    }else if(this.jumping == true){
      this.y += this.velocity;
    }
    // this.velocity *= 0.9;

    if (this.y > height) {
      this.y = height;
      this.velocity = 0;
    }

    if (this.y < 0) {
      this.y = 0;
      this.velocity = 0;
    }

  }

}
