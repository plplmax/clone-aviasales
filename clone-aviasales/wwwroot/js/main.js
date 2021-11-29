$(function () {
    let originIata = $('input[name = origin_iata]'),
        destinationIata = $('input[name = destination_iata]'),
        departDate = $('input[name = depart_date]');
    const locale = 'ru-RU';
    const purchaseUrl = 'https://aviasales.ru';
    const imagesUrl = 'https://pics.avs.io/al_square/36/36/';
    const minutesPerDay = 60 * 24;
    const minutesPerHour = 60;
    const millisecondsPerMinute = 60 * 1000;
    const fetchTimeFormatOptions = (timezone) => ({ hour: '2-digit', minute: '2-digit', timeZone: timezone });
    const fetchDateFormatOptions = (timezone) => ({ weekday: 'short', day: 'numeric', month: 'short', year: 'numeric', timeZone: timezone });

    $('#search-form').submit(function (event) {
        event.preventDefault();

        if (isFormValid()) {
            let formData = $(this).serialize();

            fetchTickets(formData);
        }
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
            success: function (data) {
                handleResponse(data);
            },
            error: function (error) {
                hideTickets();
                clearTickets();
                showNotFoundError();
            }
        });
    }

    function handleResponse(response) {
        clearTickets();

        if (isResponseValid(response)) {
            hideNotFoundError();
            showTickets();
            loadTickets(response);
            return;
        }

        hideTickets();
        showNotFoundError();
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

    function showNotFoundError() {
        $('.not-found').css('display', 'block');
    }

    function hideNotFoundError() {
        $('.not-found').css('display', 'none');
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

        for (ticket of tickets) {
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
})