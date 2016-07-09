  
jQuery.expr[':'].ContainsCaseInsensitive = function (a, i, m) { return jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase()) >= 0; };
jQuery.expr[':'].Equals = function (a, i, m) { return (jQuery(a).text().toUpperCase() == m[3].toUpperCase()); };
jQuery.fn.center = function () { this.css("position", "absolute"); this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px"); this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px"); return this; }
jQuery.fn.CenterInsideAnother = function (Parent) { this.css("position", "absolute"); this.css("top", (Parent.height() - this.height()) / 2 + Parent.scrollTop() + Parent.offset().top + "px"); this.css("left", (Parent.width() - this.width()) / 2 + Parent.scrollLeft() + Parent.offset().left + "px"); return this; }
jQuery.fn.hasScrollBar = function () {
    return this.get(0) ? this.get(0).scrollHeight > this.innerHeight() : false;
};





jQuery.expr[':'].ContainsWordByWord = function (SearcheableText, i, m) {
    var TextData = jQuery(SearcheableText).text().toUpperCase();
    var Keywords = m[3].toUpperCase();
    var ResultIndex = 0;
    var Splitted = Keywords.split(' ')
    for (ItemIndex in Splitted)
    {
        if (TextData.indexOf(Splitted[ItemIndex]) > 0 || Splitted[ItemIndex] == '') ResultIndex += 1;

    }


    return ResultIndex == Splitted.length;
};

//************************************* array query extensions

/***
var ids = this.fruits.select(function(v){
    return v.Id;
});
***/
Array.prototype.select = function (expr) {
    var arr = this;
    //do custom stuff
    return arr.map(expr); //or $.map(expr);
};

/*

var persons = [{ name: 'foo', age: 1 }, { name: 'bar', age: 2 }];

// returns an array with one element:
var result1 = persons.where({ age: 1, name: 'foo' });

// returns the first matching item in the array, or null if no match
var result2 = persons.firstOrDefault({ age: 1, name: 'foo' }); 


*/
Array.prototype.where = function (filter) {

    var collection = this;

    switch (typeof filter)
    {

        case 'function':
            return $.grep(collection, filter);

        case 'object':
            for (var prop in filter)
            {
                if (!filter.hasOwnProperty(prop))
                    continue; // ignore inherited properties

                collection = $.grep(collection, function (item) {
                    return item[prop] === filter[prop];
                });
            }
            return collection.slice(0); // copy the array 
            // (in case of empty object filter)

        default:
            throw new TypeError('func must be either a' +
                'function or an object of properties and values to filter by');
    }
};


Array.prototype.firstOrDefault = function (func) {
    return this.where(func)[0] || null;
};

/****************************************          Ensure Loghelper loaded         *******************************************/

$(document).ready(function () {

    //no need to fail just for hiding loading panel function
    if (typeof IsLogHelperLoaded == 'undefined' || !IsLogHelperLoaded)
    {
        self.alert('Log Not Found PLease let administrator know about this issue!');
    }
     
});
  


function ShowJqToast(Message) {
    try
    {
        var Lines = CountMatch(Message, '<br />', 0);
        var TotalHeight = Lines * 100
        if (TotalHeight > 500) TotalHeight = 300;
        if (TotalHeight < 250) TotalHeight = 100;
        var TotalWidth = $(window).width();// 3/4 of the screen
       

        if ($('#JqToast').length == 0)
        {
            $('body').append("<div id='JqToast' class='JqToast text-center' style='width:" + TotalWidth
                + ",height:" + TotalHeight + "'></div>");

            $('#JqToast').append("<div id='ToastText' class='ToastText'>" + Message + "</div>");
        }
        $('.JqToast').width(TotalWidth + 'px');

        $('.JqToast').center(); //this  is the div of the dialog
        $('.JqToast').css('left', 0);//bug in center function
        $('.JqToast').css({
            'display': 'block',
            'z-index': $('div').GetMaxZindex() + 3
        });

        $(".JqToast").animate({
            "opacity": "1"
        }, { queue: false, duration: 1000 }, 'swing', function () { $('.JqToast').center(); });
        $('#ToastText').html(Message);

        setTimeout(function () {

            $(".JqToast").animate({
                "opacity": "0"
            }, 1000, 'swing', function () {
                $(".JqToast").css("display", "none");
            });

        }, 5000);

    

    }
    catch (ex)
    {
        Loghelper.HandleException("InitUploadTool", ex)
    }

}


function ShowJqToast(Message) {
    try {
        var Lines = CountMatch(Message, '<br />', 0);
        var TotalHeight = Lines * 100
        if (TotalHeight > 500) TotalHeight = 300;
        if (TotalHeight < 250) TotalHeight = 100;
        var TotalWidth = $(window).width();// 3/4 of the screen
        // if (TotalWidth > 800) TotalWidth = 800;


        if ($('#JqToast').length == 0) {
            $('body').append("<div id='JqToast' class='JqToast text-center' style='width:" + TotalWidth
                + ",height:" + TotalHeight + "'></div>");

            $('#JqToast').append("<div id='ToastText' class='ToastText'>" + Message + "</div>");
        }
        $('.JqToast').width(TotalWidth + 'px');

        $('.JqToast').center(); //this  is the div of the dialog
        $('.JqToast').css('left', 0);//bug in center function
        $('.JqToast').css({
            'display': 'block',
            'z-index': $('div').GetMaxZindex() + 3
        });

        $(".JqToast").animate({
            "opacity": "1"
        }, { queue: false, duration: 1000 }, 'swing', function () { $('.JqToast').center(); });
        $('#ToastText').html(Message);

        setTimeout(function () {

            $(".JqToast").animate({
                "opacity": "0"
            }, 1000, 'swing', function () {
                $(".JqToast").css("display", "none");
            });

        }, 5000);
 

    }
    catch (ex) {
        alert(ex);
    }
}

function ShowJqMsgBox(Message, Title) {
    try
    {
        $('.JqMessagBox').css("display", "none");
        if ($('#JqMessagBox').length == 0)
        {
            $('body').append("<div id='JqMessagBox' class='JqMessagBox'></div>");


            $('#JqMessagBox').append("<div id='MessageText' class='MessageText'>" + Message + "</div>");
            $('#JqMessagBox').append("<div id='MessageControlBar' class='MessageControlBar'>" +
                "<button onclick='CloseJqMsgBox();'>Close</button></div>");


        }
        var Lines = CountMatch(Message, '<br />', 0);
        var TotalHeight = Lines * 50
        if (TotalHeight > 500) TotalHeight = 500;
        if (TotalHeight < 250) TotalHeight = 250;
        var TotalWidth = $(window).width() * 0.75;// 3/4 of the screen
        if (TotalWidth > 800) TotalWidth = 800;
        var LoadingBox = $(".JqMessagBox").dialog({
            width: TotalWidth,
            height: TotalHeight,
            modal: true,
            title: Title
        });
         $('.ui-dialog').center(); //this  is the div of the dialog
        $('#MessageText').html(Message);

    }
    catch (ex)
    {
        Loghelper.HandleException("InitUploadTool", ex)
    }

}

function CountMatch(string, subString, allowOverlapping) {

    string += ""; subString += "";
    if (subString.length <= 0) return string.length + 1;

    var n = 0, pos = 0;
    var step = (allowOverlapping) ? (1) : (subString.length);

    while (true)
    {
        pos = string.indexOf(subString, pos);
        if (pos >= 0) { n++; pos += step; } else break;
    }
    return (n);
}
function CloseJqMsgBox() {
    $(".JqMessagBox").dialog('close');

    $('#MessageText').html("");
    $("#MessageControlBar").hide();



}
 