window.addEventListener("load", function(event) {

  var carousel = document.querySelector('.carousel');
  var bod = document.getElementById("body1");
  var cellCount = 9;
  var selectedIndex = 0;
  var switchIndex = 0;
  var cellElements = document.getElementsByClassName("song");
  var prevButton = document.querySelector('.previous-button');
  prevButton.addEventListener( 'click', function() {
    selectedIndex--;
    switchIndex--;
    if(switchIndex == -1){
      switchIndex = 8;
    }
    rotateCarousel();
  });

  var nextButton = document.querySelector('.next-button');
  nextButton.addEventListener( 'click', function() {
    selectedIndex++;
    switchIndex++;
    if(switchIndex == 9){
      switchIndex = 0;
    }
    rotateCarousel();
  });


  function rotateCarousel() {

    var angle = selectedIndex / cellCount * -360;
    carousel.style.transform = 'translateZ(-288px) rotateY(' + angle + 'deg)'+' scale3d(1.5,1.5,1.5)';
    /* make the audio element visible on the nth child carousel__cell*/
    var i;
    for (i = 0; i < cellElements.length; i++) {
      cellElements[i].hidden = true;
    }
    cellElements[switchIndex].hidden = false;
    /*selectedIndex = 0-9, use with switch case to set background image*/
    switch(switchIndex){
        case 0:
        /*hawaii*/
          document.body.style.backgroundImage = "linear-gradient(80deg, #22B2B2 30%, #96DEDA 80%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(80deg, #22B2B2 30%, #96DEDA 80%)";
          document.body.style.backgroundImage = "-o-linear-gradient(80deg, #22B2B2 30%, #96DEDA 80%)";

          break;
        case 1:
        /*slowDancing*/
          document.body.style.backgroundImage = "linear-gradient(360deg,#00537E 10%,#3AA17E 100%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(360deg,#00537E 10%,#3AA17E 100%)";
          document.body.style.backgroundImage = "-o-linear-gradient(360deg,#00537E 10%,#3AA17E 100%)";
          break;
        case 2:
        /*carpeDiem*/
          document.body.style.backgroundImage = "linear-gradient(90deg, #b6eae1 10%, #D2FBAD 100%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(90deg, #b6eae1 10%, #D2FBAD 100%)";
          document.body.style.backgroundImage = "-o-linear-gradient(90deg, #b6eae1 10%, #D2FBAD 100%)";
          break;
        case 3:
          /*colorado*/
          document.body.style.backgroundImage = "linear-gradient(90deg, #38aecc 40%, #347fb9 100%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(90deg, #38aecc 40%, #347fb9 100%)";
          document.body.style.backgroundImage = "-o-linear-gradient(90deg, #38aecc 40%, #347fb9 100%)";
          break;
        case 4:
          /*underwater*/
          document.body.style.backgroundImage = "linear-gradient(270deg,#224e4d 10%,#083023 70%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(270deg,#224e4d 10%,#083023 70%)";
          document.body.style.backgroundImage = "-o-linear-gradient(270deg,#224e4d 10%,#083023 70%)";
          break;
        case 5:
        /*inception*/
          document.body.style.backgroundImage = "linear-gradient(270deg, #dee1e1 10%, #5d6578 90%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(270deg, #dee1e1 10%, #5d6578 90%)";
          document.body.style.backgroundImage = "-o-linear-gradient(270deg, #dee1e1 10%, #5d6578 90%)";
          break;
        case 6:
        /*tronslow*/
          document.body.style.backgroundImage = "linear-gradient(90deg,#383836 40%,#3d3dff 250%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(90deg,#383836 40%,#3d3dff 250%)";
          document.body.style.backgroundImage = "-o-linear-gradient(90deg,#383836 40%,#3d3dff 250%)";
          break;
        case 7:
        /*tronfast*/
          document.body.style.backgroundImage = "linear-gradient(90deg,#383836 70%,#81d4fa 110%)";
          document.body.style.backgroundImage = "-webkit-linear-gradient(90deg,#383836 70%,#81d4fa 110%)";
          document.body.style.backgroundImage = "-o-linear-gradient(90deg,#383836 70%,#81d4fa 110%)";
          break;
        case 8:
          break;
      default:
        alert(switchIndex);
    }

  }
});
