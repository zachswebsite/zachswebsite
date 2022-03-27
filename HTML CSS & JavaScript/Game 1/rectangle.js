function Rectangle(x,y,w,h){
  this.x = x;
  this.y = y;
  this.width = w;
  this.height = h;
  this.speed = 4;
  this.highlight = false;
  this.leftX = this.x;
  this.rightX = this.x+w;
  this.topY = this.y;
  this.botY = this.y+h;
  
  this.show = function(){
    fill("Black");
    rect(this.x,this.y,this.width,this.height);
    
  }
  
  this.update = function(){
    this.x -= this.speed;
    this.leftX = this.x;
    this.topY = this.y+(h/2);
    this.rightX = this.x+this.width;
  
    
  }
  
  this.onScreen = function(){
    //check if far left and right x values are within the screen width
    if((this.leftX > 0 && this.leftX < width)||(this.rightX >0 && this.rightX < width)){
      return true;
    }else{
      //now check for large rectangles that cross screen width
      if(this.leftx <= 0 && this.rightX >= width){
        return true;
      }else{
        return false;
      }
    } 
  }
  
  this.hits = function(player){
    //check for x bound
    if((player.rightX >= this.leftX && player.rightX <= this.rightX) ||(player.leftX <= this.rightX && player.leftX >= this.leftX)){
      //now check y bound
      if((player.topY >= this.topY && player.topY <= this.botY) || (player.botY >= this.topY && player.botY <= this.botY)){
        this.highlight = true;
        return true;
      }
    }
    this.highlight = false;
    return false;
    
  }
  
  this.lands = function(player){
    
    //skateboard lands on the top of rectangle, has to be top...
    //check left and right skateboard x collision points
    if(player.velocity >= 0){
    if((player.boardX-40 >= this.leftX && player.boardX-40 <= this.rightX)||(player.boardX+10 >= this.leftX && player.boardX+10 <= this.rightX)){
      //now check y collision
      if(player.boardY >= this.topY){
        //print("PLAYER LANDED");
        player.landed = true;
        return true;
      }
    }
    }
    player.landed = false;
    return false;
  }
  
}