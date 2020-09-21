window.initializeCarousel = () => {
    $('#carouselExampleIndicators').carousel({ interval: false, touch: true });

    //see step 2 to understand these news id's:
    $('#carouselExampleIndicators-prev').click(
        () => $('#carouselExampleIndicators').carousel('prev'));
    $('#carouselExampleIndicators-next').click(
        () => $('#carouselExampleIndicators').carousel('next'));

}