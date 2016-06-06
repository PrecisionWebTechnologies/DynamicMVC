//autocomplete
$(document).ready(function () {
    $('.autocomplete').each(function (index, element) {
        $(element).select2({
            minimumInputLength: 1,
            ajax: {
                url: $(element).attr('data-url'),
                type: "POST",
                dataType: "json",
                data: function (term, page) {
                    return {
                        searchstring: term
                    }
                },
                results: function (data, page) {
                    return { results: data };
                },
                error: function (xhr) {
                    alert('There was an error trying to retreive data for the autocomplete control.');
                }
            }
        });
        var dv = $(element).attr('data-default');
        var id = $(element).attr('data-id');
        if (dv !== undefined && id !== undefined) {
            $('#s2id_' + id + ' .select2-chosen').text(dv);
            $('#s2id_Item_' + id + ' .select2-chosen').text(dv);
        }
    });

    $('.datepicker').datepicker();
});

function UpdateMobileIndexView() {
    var pagingMessage = $('#DesktopPagingMessage').text();
    $('#MobilePagingMessage').text(pagingMessage);
   
    //enabled or disabled
    var previousDisabled = $('#desktopPrevious').hasClass('disabled');
    if (previousDisabled===true) {
        $('#mobilePrevious').addClass('disabled');
    } else {
        $('#mobilePrevious').removeClass('disabled');
    }
    var nextDisabled = $('#desktopNext').hasClass('disabled');
    if (nextDisabled === true) {
        $('#mobileNext').addClass('disabled');
    } else {
        $('#mobileNext').removeClass('disabled');
    }

    //update ajax url
    var previousUrl = $('#desktopPreviousLink').data('ajax-url');
    var nextUrl = $('#desktopNextLink').data('ajax-url');

    $('#mobilePrevious').attr('data-ajax-url', previousUrl);
    $('#mobileNext').attr('data-ajax-url', nextUrl);

}

//mobile filter panel
$(document).on('click', '.panel-heading span.clickable', function(e) {
    var $this = $(this);
    ToggleCollapse($this);
});

$(document).ready(function() {
    $('.panel-heading span.clickable').click();
});

function ToggleCollapse(collapsable) {
    var $this = collapsable;
    if (!$this.hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').slideUp();
        $this.addClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
    } else {
        $this.parents('.panel').find('.panel-body').slideDown();
        $this.removeClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
    }
}

//$(document).ready(function() {
//    $('.panel-heading span.clickable').ToggleCollapse();
//});
