$(function () {
    let originIata = $('input[name = origin_iata]'),
        destinationIata = $('input[name = destination_iata]'),
        departDate = $('input[name = depart_date]');
    const locale = 'ru-RU';
    const purchaseUrl = 'https://aviasales.ru';
    const imagesUrl = 'https://pics.avs.io/al_square/36/36/';
    const mobileImagesUrl = 'https://mpics.avs.io/al_square/48/48/';
    const minutesPerDay = 60 * 24;
    const minutesPerHour = 60;
    const millisecondsPerMinute = 60 * 1000;
    const fetchTimeFormatOptions = (timezone) => ({ hour: '2-digit', minute: '2-digit', timeZone: timezone });
    const fetchDateFormatOptions = (timezone) => ({ weekday: 'short', day: 'numeric', month: 'short', year: 'numeric', timeZone: timezone });
    // add new on submit and checkboxes for filters, may be
    // hide passengers input
    $('#duration-range').on('change', function () {
        $('input[name="filters[duration]"]').val($(this).val());
        $('input[name="filters[duration]"').val() == $(this).attr('max') ? $('input[name="filters[duration]"').prop('checked', false) : $('input[name="filters[duration]"').prop('checked', true);;
        $('#filters-form').submit();
        console.log("change");
    });

    $('#duration-range').on('input', function () {
        $('.duration-filter span').text("До " + $(this).val() + "ч");
        console.log("input");
    });

    $('#search-form').submit(function (event) {
        event.preventDefault();

        if (isFormValid()) {
            let formData = $(this).serialize();
            fetchTickets(formData);
        }
    });

    $('#filters-form').submit(function (event) {
        event.preventDefault();

        if (isFormValid()) {
            let formData = $('#search-form').serialize();
            let filterData = $(this).serialize();
            formData += "&" + filterData;
            fetchTicketsWithFilters(formData);
        }
    });

    $('input[name="filters[transfers_count]"]').on('click', function () {
        $('#filters-form').submit();
    });

    $('body').on('click', 'input[name = "filters[airlines]"]', function () {
        $('#filters-form').submit();
    });

    function isFormValid() {
        return originIata.val() && destinationIata.val() && departDate.val();
    }

    function isResponseValid(response) {
        return response.success === true &&
            response.data != null &&
            response.data.length != 0 &&
            response.airlines != null &&
            response.airlines.length != 0 &&
            response.cities != null &&
            response.cities.length != 0;
    }

    function fetchTickets(formData) {
        $.ajax({
            type: 'GET',
            url: 'api/ticket',
            dataType: 'json',
            data: formData,
            success: (data) => handleResponse(data),
            error: (error) => {
                hideTickets();
                clearTickets();
                hideWelcomeCard();
                showNotFoundError();
            },
            beforeSend: () => {
                $('.loader').show();
                hideWelcomeCard();
                hideNotFoundError();
                hideTickets();
                hideFlightFilters();
            },
            complete: () => $('.loader').hide()
        });
    }

    function fetchTicketsWithFilters(formData) {
        $.ajax({
            type: 'GET',
            url: 'api/ticket',
            dataType: 'json',
            data: formData,
            success: (data) => handleResponseWithFilters(data),
            error: (error) => {
                hideTickets();
                clearTickets();
                showStrangeFiltersCard();
            },
            beforeSend: () => $('.flight').toggleClass('show'),
            complete: () => $('.flight').toggleClass('show')
        });
    }


    function handleResponse(response) {
        clearTickets();
        clearAirlines();

        if (isResponseValid(response)) {
            hideNotFoundError();
            hideWelcomeCard();
            showFlightFilters();
            showTickets();
            loadTickets(response);
            loadAirlines(response);
            updateDurationRange(response);
            return;
        }

        hideTickets();
        hideFlightFilters();
        hideWelcomeCard();
        showNotFoundError();
    }

    function handleResponseWithFilters(response) {
        clearTickets();

        if (isResponseValid(response)) {
            hideStrangeFiltersCard();
            showTickets();
            loadTickets(response);
            return;
        }

        hideTickets();
        showStrangeFiltersCard();
    }

    function hideTickets() {
        $('.flight').css('display', 'none');
    }

    function showTickets() {
        $('.flight').css('display', 'block');
    }

    function clearTickets() {
        $('.flight').empty();
    }

    function clearAirlines() {
        $('.airlines-filter').empty();
    }

    function showNotFoundError() {
        $('.not-found-card').css('display', 'block');
    }

    function hideNotFoundError() {
        $('.not-found-card').css('display', 'none');
    }

    function hideWelcomeCard() {
        $('.welcome-card').css('display', 'none');
    }

    function showWelcomeCard() {
        $('.welcome-card').css('display', 'block');
    }

    function showFlightFilters() {
        $('.flight-filters').css('display', 'block');
    }

    function hideFlightFilters() {
        $('.flight-filters').css('display', 'none');
    }

    function hideStrangeFiltersCard() {
        $('.strange-filters-card').css('display', 'none');
    }

    function showStrangeFiltersCard() {
        $('.strange-filters-card').css('display', 'block');
    }

    function convertMinutesToDuration(flightDuration) {
        let days = Math.floor(flightDuration / (minutesPerDay));
        let hours = Math.floor(flightDuration % (minutesPerDay) / minutesPerHour);
        let minutes = Math.floor(flightDuration % minutesPerHour);

        return [days, hours, minutes];
    }

    function loadTickets(response) {
        let tickets = response.data;
        let cities = response.cities;
        let airlines = response.airlines;
        let maxDuration = tickets[0].duration;

        for (ticket of tickets) {
            if (maxDuration < ticket.duration) maxDuration = ticket.duration;

            let flightDurationInMinutes = Number(ticket.duration);
            let flightDuration = convertMinutesToDuration(flightDurationInMinutes);

            let days = flightDuration[0];
            let hours = flightDuration[1];
            let minutes = flightDuration[2];

            let origin = cities[ticket.origin].name;
            let destination = cities[ticket.destination].name;
            let price = ticket.price;
            let link = ticket.link;
            let departDate = new Date(Date.parse(ticket.departure_at));
            let departDateFinish = new Date(departDate.getTime() + flightDurationInMinutes * millisecondsPerMinute);
            let returnDate = ticket.return_at ? new Date(Date.parse(ticket.return_at)) : ticket.return_at;

            let returnContainer = '<div class="flight-content__description d-flex mt-3 align-items-center border-top pt-3">' +
                '<div class="flight-content__origin lh-base flex-basis-100">' +
                '<p class="fs-3">' + new Intl.DateTimeFormat(locale, fetchTimeFormatOptions(cities[ticket.destination].timezone)).format(returnDate) + '</p>' +
                '<p class="fs-6 text-secondary">' + destination + '</p>' +
                '<p class="fs-6 text-secondary">' + new Intl.DateTimeFormat(locale, fetchDateFormatOptions(cities[ticket.destination].timezone)).format(returnDate) + '</p>' +
                '</div>' +
                '<div class="flight-content__duration mx-5 text-center flex-basis-100">' +
                '<img src="./images/help_black_48dp.svg" data-bs-toggle="tooltip" data-bs-placement="top" title="При покупке указано время в пути" class="custom-img me-2 rounded-pill">' +
                '</div>' +
                '<div class="flight-content__destination text-end lh-base flex-basis-100">' +
                '<img src="./images/help_black_48dp.svg" data-bs-toggle="tooltip" data-bs-placement="top" title="При покупке указано время прибытия" class="custom-img me-2 rounded-pill">' +
                '<p class="fs-6 text-secondary">' + origin + '</p>' +
                '<img src="./images/help_black_48dp.svg" data-bs-toggle="tooltip" data-bs-placement="top" title="При покупке указана дата прибытия" class="custom-img me-2 rounded-pill">' +
                '</div>' +
                '</div>';

            let html = '<div class="card border-light rounded-15 ms-4 mb-4">' +
                '<div class="card-body d-flex p-0">' +
                '<div class="flight-price border-end p-3 d-flex align-items-center">' +
                '<a class="btn btn-primary py-1 px-5" href="' + purchaseUrl + link + '">Купить за <p>' + price + ' BYN</p></a>' +
                '</div>' +
                '<div class="flight-content ms-3 p-3">' +
                '<div class="flight-content__airlines">' +
                '<img src="' + imagesUrl + ticket.airline + '@2x.png" data-bs-toggle="tooltip" data-bs-placement="top" title="' + (airlines[ticket.airline]?.name ?? "Неизвестно") + '" class="custom-img me-2 rounded-pill">' +
                '</div>' +
                '<div class="flight-content__description d-flex mt-3 align-items-center">' +
                '<div class="flight-content__origin lh-base">' +
                '<p class="fs-3">' + new Intl.DateTimeFormat(locale, fetchTimeFormatOptions(cities[ticket.origin].timezone)).format(departDate) + '</p>' +
                '<p class="fs-6 text-secondary">' + origin + '</p>' +
                '<p class="fs-6 text-secondary">' + new Intl.DateTimeFormat(locale, fetchDateFormatOptions(cities[ticket.origin].timezone)).format(departDate) + '</p>' +
                '</div>' +
                '<div class="flight-content__duration mx-5 text-center">' +
                '<span>В пути: ' + (days ? days + 'д ' : "") + (hours ? hours + 'ч ' : "") + (minutes ? minutes + 'м' : "") + '</span>' +
                '<p>(пересадок: ' + ticket.transfers + ')</p>' +
                '</div>' +
                '<div class="flight-content__destination text-end lh-base">' +
                '<p class="fs-3">' + new Intl.DateTimeFormat(locale, fetchTimeFormatOptions(cities[ticket.destination].timezone)).format(departDateFinish) + '</p>' +
                '<p class="fs-6 text-secondary">' + destination + '</p>' +
                '<p class="fs-6 text-secondary">' + new Intl.DateTimeFormat(locale, fetchDateFormatOptions(cities[ticket.destination].timezone)).format(departDateFinish) + '</p>' +
                '</div>' +
                '</div>' +
                (returnDate ? returnContainer : "")
                '</div>' +
                '</div>' +
                '</div>';

            $('.flight').append(html);
        }

        //todo: Сделать код красивее

        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl)
        })
    }

    function loadAirlines(response) {
        let airlines = response.airlines;
        for (airline in airlines) {
            let html = '<label class="list-group-item border-0 d-flex align-items-center">' +
                '<img src="' + mobileImagesUrl + airline + '.png" class="aero-filter-img me-2 rounded-pill">' +
                '<span>' + airlines[airline].name + '</span>' +
                '<input name="filters[airlines]" class="form-check-input me-1 mt-0 position-absolute end-0" type="checkbox" value="' + airline + '">' +
                '</label>';
            $('.airlines-filter').append(html);
        }
    }

    function updateDurationRange(response) {
        let maxDuration = fetchMaxDuration(response.data);
        let durationInHours = Math.ceil(maxDuration / 60);
        $('.duration-filter span').text("До " + durationInHours + "ч");
        $('#duration-range').attr('max', durationInHours);
        $('#duration-range').val(durationInHours);
    }

    function fetchMaxDuration(tickets) {
        let maxDuration = tickets[0].duration;
        for (ticket of tickets) {
            if (maxDuration < ticket.duration) maxDuration = ticket.duration;
        }

        return maxDuration;
    }
})