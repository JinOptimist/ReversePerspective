/**
 * Created by artsiom_papou on 3/3/2016.
 */
;
$(document).ready(function() {
    var nav = $('nav');
    var navInTop = false; //navigation position top?
    var mainScreen = true;
    var ancorClicked = false; //scrolling by ancor clicking?
    var positionDif = 0; //varable for changing vertical menu position bu translateY
    var pastScrollTop = 0;
    var currentMousePos = {x: -1, y: -1};
    var vertMenuFreezePos; //Save pre-traslated position for vertical menu
    var mouseInVertMenu = false;
    var filmData = {topItem : 0, itemsValue : 0 };
    var offsetTop = 200; //Отметка сверху, после достижения которой, при скролле, меняется положение верт-ого меню

/**********Start initialization***************/
    navItemWindth();
    $('.films-list li h3').after('<div class="img-wrapper"></div>');

    $('.films-list li h3').each(function () {
        getFilmInfo($(this).text());
    });

    filmData.data = createItemInfo($('.films-list li'));
    setIndexToData('.scroll-nav li a');

/************Data methods**********************/

    filmData.next = function(){
        this.topItem++;
    };
    filmData.prev = function(){
        this.topItem--;
    };
    filmData.checkItemPos = function(itemNum, currentScrollTop){
        var item = itemNum;
        var scroll = currentScrollTop;
        var navPos = parseInt($('.scroll-nav').css('top'));

        /****Если блок в топе страницы***/
        if(this.data[item].startY < scroll && this.data[item].endY > scroll ){

            if(navPos == this.data[item].AncorTranslateY) return false; //Если вертикальное меню в нужной поз-ции, ничего не делаем
            else this.changeTopPos(item); //если нет меняем
        }
        else this.changeItem(scroll);
    };
    /*определяет элемент вертикального мню, который нужно сделать активным, меняет стили для активного элемента*/
    filmData.changeItem = function(scrollTop) {
        //debugger
        var currentScroll = scrollTop;
        var currentItem = this.topItem;
        var itemToBold;

        itemToBold = $('.scroll-nav li a')[this.topItem];
        $(itemToBold).removeClass('active');

        if(currentScroll < this.data[currentItem].startY){
            if(this.topItem > 0) {
                this.prev();
                this.changeTopPos(this.topItem);
            } else return;

        } else
        if(currentScroll > this.data[currentItem].endY) {
            this.next();
            this.changeTopPos(this.topItem);
        }
        /*************************************************/
        itemToBold = $('.scroll-nav li a')[this.topItem];
        if (!$(itemToBold).hasClass('active')){
            setTimeout(function () {
                $(itemToBold).addClass('active');
            },5)

        }
    };
    /*меняет позицию вертикального меню*/
    filmData.changeTopPos = function(itemNum){
        var navPanel = $('.scroll-nav');
        var newPos = this.data[itemNum].AncorTranslateY;
        navPanel.css('top', -newPos + 'px');
    };
    filmData.refresh = function (filmsBlock) {
        var list = $(filmsBlock);
        var scrNav = $('.scroll-nav li');
        filmData.itemsValue = list.length-1;

        for (var i = 0; i < list.length; i++){
            var temp = $(scrNav[i]);
                this.data[i].startY = $(list[i]).offset().top;
                this.data[i].endY = $(list[i]).outerHeight() + $(list[i]).offset().top;
                this.data[i].AncorTranslateY = i*temp.outerHeight(true)-offsetTop;
        }
    };
    filmData.downloadPoster = function (currnetItem) { //загружает постер для след. фильма
        var nextItem = currnetItem + 1;
        if (currnetItem == 0) {
            nextItem = 0;
            /*100мс для загрузки картинки*/
            setTimeout(function () {
                if(filmData.data[nextItem].filmInfo.filmPoster) {
                    $('#film' +(nextItem+1) + ' .img-wrapper').css({
                        'background':'url(' + filmData.data[nextItem].filmInfo.filmPoster + ') no-repeat',
                        backgroundSize : 'contain'
                    });
                }
            },100);

        } else{
            /*100мс для загрузки картинки*/
            setTimeout(function () {
                if(filmData.data[nextItem].filmInfo.filmPoster) {
                    $('#film' +(nextItem + 1) + ' .img-wrapper').css({
                        'background':'url(' + filmData.data[nextItem].filmInfo.filmPoster + ') no-repeat',
                        backgroundSize : 'contain'
                    });
                }
            },100);
        }
    };


/*************GLobal Events**************/

    $(window).resize(function(){
        navItemWindth();
        filmData.refresh('.films-list li');
        //filmData.data = createItemInfo($('.films-list li'));

        if($(window).width() > 1000) {
            $('.nav-mobile-button').removeClass('nmb-active');
            $('.nav-mobile').hide();
        }
        if ($(window).width() <= 620 && !$('.nav-mobile-button').hasClass('nmb-active')) {
            $('.logo-wrap').hide();
        }else {
            $('.logo-wrap').show();
        }

    });
    $(document).mousemove(function(event) {
        var vertMenu = $('.scroll-nav ');

        currentMousePos.x = event.pageX;
        currentMousePos.y = event.pageY;

        if(currentMousePos.x > $('.scroll-nav').offset().left &&
            currentMousePos.x < $('.scroll-nav').offset().left + $('.scroll-nav').width() &&
            currentMousePos.y > $('.scroll-nav').offset().top &&
            currentMousePos.y < $('.scroll-nav').offset().top + $('.scroll-nav').outerHeight()) {

            if(!mouseInVertMenu) {
                vertMenuFreezePos = parseInt($('.scroll-nav').css('transform').split(',')[5]);
                positionDif = vertMenuFreezePos;
                mouseInVertMenu = true;
            }
            $('.scroll-nav').unbind('mousewheel DOMMouseScroll').on('mousewheel DOMMouseScroll', function(event) {

                var delta = event.originalEvent.detail < 0 || event.originalEvent.wheelDelta > 0 ? 1 : -1;

                if (delta < 0) {
                    positionDif = positionDif - 120 ;
                    vertMenu.css({transform : 'translateY(' + positionDif  +'px)'} );
                } else {
                    positionDif = positionDif + 120 ;
                    vertMenu.css({transform : 'translateY(' + positionDif +'px)'} );
                }
                return false;
            });
        } else {
            if (mouseInVertMenu) {
                mouseInVertMenu = false;
                positionDif = 0;
                vertMenu.css({transform : 'translateY(' + positionDif  +'px)'} );
            }
        }
    });
    $(window).scroll(function(e){

        if ($('body').scrollTop() == 0 && mainScreen == false) {
            setTimeout(function(){mainScreen = true;},100)
        }

        if (mainScreen && !ancorClicked) {
            $('body').scrollTop($(window).height()-140);
            mainScreen = false;
        }
        if ($('body').scrollTop() > 0 && !navInTop) {
            nav.css({top: 0});
            navInTop = true;
            nav.css({position: 'fixed'});
            nav.addClass('nav-to-top');
            navItemWindth();
        }
        if ($('body').scrollTop() == 0 && navInTop) {
            nav.css({position: 'absolute'});
            nav.css({top: ($(window).height() - 40) + 'px'});
            navInTop = false;
            nav.removeClass('nav-to-top');
            navItemWindth();
        }

        if($('body').scrollTop()+offsetTop > filmData.data[0].startY &&
            $('body').scrollTop()+offsetTop < filmData.data[filmData.itemsValue].endY) {
            if (!$('.scroll-nav').hasClass('nav-open')) {
                $('.scroll-nav').addClass('nav-open');

                setTimeout(function () {
                    $('.scroll-nav').animate({
                        'left' : '50px'
                    }, 300);
                },100)

            }
            
            filmData.refresh('.films-list li');
            filmData.checkItemPos(filmData.topItem, $('body').scrollTop()+200);


        } else {
            if($('.scroll-nav').hasClass('nav-open')) {
                $('.scroll-nav').animate({
                    'left' : '-200px'
                }, 100);

                $('.scroll-nav').removeClass('nav-open');
            }
        }
        if($('body').scrollTop()+300 > filmData.data[filmData.topItem].startY &&
            $('body').scrollTop() < filmData.data[filmData.itemsValue].endY) {
            filmData.downloadPoster(filmData.topItem);
        }

        progressBar();
        pastScrollTop = $('body').scrollTop();

    });

/****************Set Events and styles********************************/
    //$('.scroll-nav').css('top', filmData.data[0].posY);
    $('.main-page').css('height', '100vh');
    $('.nav-mobile-button').on('click', function(){
        $(this).toggleClass('nmb-active');
        if($('.nav-mobile-button').hasClass('nmb-active')){
            $('.nav-mobile').show();
            if ($(window).width() <= 620) {
                $('.logo-wrap').show();
            }

        }else {
            $('.nav-mobile').hide();
            if($(window).width() <= 620) {
                $('.logo-wrap').hide();
            }
        }
    });
    $('nav a').each(function(){
        goToAncor(this);
    });
    $('.nav-mobile a').add('.logo-wrap a').each(function(){
        goToAncor(this, -1);
    });
    $('.scroll-nav a').each(function(){
        var parent =  $(this).parent();
        goToAncor(this, 140);
        /*$(this).on('click', function (e) {
            e.preventDefault();
            verticalAncorGoTo(e, filmData);
        });*/

        $(this).hover(function(){
            parent.addClass('scroll-nav-item_hovered')
        });
        $(this).on('mouseleave',function(){
            parent.removeClass('scroll-nav-item_hovered')
        });
    });

/************Helpers****************/
/*Recalculate width of main navigation's items*/
    function navItemWindth(){
        var windowWidth = $(window).width();
        var navItemCount = 5;// or $(.nav-item).length
        var atTop = false;

        atTop = $('nav').hasClass('nav-to-top') ? true : false;

        if(!atTop) {
            $('.nav-item').each(function(){
                $(this).css('width', windowWidth/navItemCount +'px');
                $(this).css('transform', 'translateX(0)');
                $(this).removeClass('nav-item-top');
            });
        } else {
            $('.nav-item').each(function(){
                //80 logo width, 20 left
                $(this).css('width', (windowWidth-100)/navItemCount +'px');
                $(this).css('transform', 'translateX(100px)');
                $(this).addClass('nav-item-top');
            });
        }
    }
    /* animate movement to ancors
    *link - navigation batton / string / selector,
    *ancor - target / string / selector,
    *marginTop - offset for menu height
    */
    function goToAncor(link, marginTop){
        var navH = marginTop ? marginTop : 140;
        var  href = $(link).attr('href');

        $(link).on('click', function scrTo(e){
            e.preventDefault();
            $('body').stop().animate({
                scrollTop: $(href).offset().top - navH
            }, 700, function(){
                mainScreen = false;
            });
        });
    }

    function setIndexToData(listOfItems) {
        var list = $(listOfItems);
        list.each(function (index) {
            $(this).attr('data-index',index);
        })
    }

    function checkAndGo(item, currentPos, endPos) {

        if(currentPos != endPos) {
            $('body').stop().animate({
                scrollTop: endPos
            }, 200, function(){
                filmData.refresh();
                if(currentPos != endPos) {
                    checkAndGo(item,$('body').scrollTop(),filmData.data[item].startY)
                } else {
                    checkItemPos(item,endPos);
                }
                mainScreen = false;
            });
        }

    }
    /*fill progressbar*/
    function progressBar(){
        var height = $(document).height();
        var currentPos =  $('body').scrollTop() + $(window).height();
        var value = currentPos / height * 100;
        var bar = $('.progress-bar');
        if ($('body').scrollTop() == 0) {
            value = 0;
        }
        if ($('body').scrollTop() >= 0) {
            bar.css('width', value + '%');
        }
        if ($('body').scrollTop() == 0) {
            value = 0;
        }
    }

    /**
     *
     * @param arr - list of films / selector
     * @returns {{}} - object with information about films'section position,
     *                  coords for scrolling nav item
     */
    function createItemInfo(arr){
        var list = $(arr);
        var scrNav = $('.scroll-nav li');
        var data = {};
        var correntTitle = '';
        filmData.itemsValue = list.length-1;


        for (var i = 0; i < list.length; i++){
            var temp = $(scrNav[i]);
            correntTitle = $(list[i]).find('h3').text();
            data[i] =  {
                filmInfo: { title: correntTitle },
                startY : $(list[i]).offset().top,
                endY : $(list[i]).outerHeight() + $(list[i]).offset().top,
                AncorTranslateY : i*temp.outerHeight(true)-offsetTop
            };
        }
        return data;
    }

    /**
     *
     * @param filmName -film title from film list h3 / string
     *
     * for every film get it's ID and poster URL &
     * push its to film data information
     */
    function getFilmInfo(filmName) {
        var filmTitle = encodeURI(filmName.split(' ').join(',')),
            filmInfo = {filmId: null, filmPoster: null},
            promise = $.get('http://api.kinopoisk.cf/searchFilms?keyword='+filmTitle);

        promise.done(function (data) {
            for(var item in filmData.data){
                if(filmData.data[item].filmInfo.title == filmName) {
                    filmData.data[item].filmInfo.filmId = data.searchFilms[0].id;
                    filmData.data[item].filmInfo.filmPoster = 'http://st.kp.yandex.net/images/' + data.searchFilms[0].posterURL.split('iphone60').join('iphone360');
                }
            }
        })
    }
});




























