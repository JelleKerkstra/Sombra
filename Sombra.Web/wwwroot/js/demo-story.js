$(document).ready(function () {
    setTimeout(function () {
        $('.profile-name').val('Judith van Middelkoop');
        $('.title').val('Mijn vrienden en ik bezochten ons sponsorkindje in Oeganda');
        $('textarea[name=story-part-1]').val('Eind 2014 kwam er een Oegandees kinderkoor naar Nederland. Het koor gaf concerten in kerken en op plaatselijke podia. Deze concerten werkten als inzamelingsactie voor een project in het dorp waar het grut vandaan kwam. De kinderen dansten en zongen de sterren van de hemel en hun enthousiasme was aanstekelijk. Ik werd ontroerd door het feit dat zij op deze manier heel erg hun best deden om hun families in het thuisland te helpen. Het was bijzonder om te zien dat ze er daadwerkelijk plezier in hadden om hun concert te geven. Ze leken alle ellende even te vergeten…');
        $('textarea[name=story-part-2]').val('Maar die ellende was er wel, in Oeganda. Achterin de zaal stond daarom een informatietafel voor Uganda Child Care Foundation: de stichting die strijdt tegen armoede in Oeganda en tevens het kinderkoor heeft opgericht. Een vriend van me las dat we een kind konden sponsoren. We keken elkaar aan en wisten dat we dus iets konden doen! Een paar berichtjes in de groepsapp en een formuliertje later, hadden we met z’n achten een sponsorkind in Uganda. De zesjarige Sinwe was door het dolle heen toen hij dit te horen kreeg! We sturen regelmatig een brief met een klein speelgoedje naar ‘ons’ sponsorkind en hij schrijft trouw terug met de hulp van zijn schooljuf. Hij is heel dankbaar dat hij door onze sponsoring naar school kan en mag leren. Daar kunnen we hier echt wat van leren!');
        $('textarea[name=story-part-3]').val('In een van de nieuwsbrieven van UCCF werd er een oproep gedaan voor vrijwilligers die in de zomer van 2015 naar Uganda wilden om mee te werken in een bouwproject. Aan dit bouwproject was een kinderkamp gekoppeld, waarvoor organisatoren gezocht werden. Ik voelde me dus meteen aangesproken. Ik heb een zwak voor kinderen, studeer ook voor docent basisonderwijs en heb daarom voldoende ervaring en inspiratie voor een kinderkamp. Mijn vrienden en ik organiseerden samen met nog drie andere vrijwilligers een kinderkamp waarin de kinderen meededen aan allerlei leuke activiteiten, terwijl hun ouders aan het bouwproject meewerkten. Sinwe was een van de zestig kinderen voor wie we het kamp organiseerden. Hij herkende ons van de meegestuurde groepsfoto en hij rende zonder enige twijfel op ons af! De tranen liepen over mijn wangen toen Sinwe me zo stevig vastpakte en me bedankte dat ik voor hem en zijn familie wilde zorgen. Ik was sprakeloos en kon alleen maar naar hem lachen.');
        $('#quote').val('De tranen liepen over mijn wangen toen Sinwe me zo stevig vastpakte en me bedankte dat ik voor hem en zijn familie wilde zorgen.');

        HeaderPicChanged("https://i.imgur.com/SKULVEK.jpg");
        $('#shape-frame').css('background-image', `url(${"https://i.imgur.com/UxOA0Tj.jpg"})`);
        MainPicChanged("https://i.imgur.com/9CPkimC.jpg");
        $('#select-your-charity')[0].selectize.setValue('77b0b03f-4ad9-4939-9057-bc80bf51202a');


        $('.send-button a').attr('href', 'http://ugcf.ikdoneer.nu/verhalen/mijn-vrienden-en-ik-bezochten-ons-sponsorkindje-in-oeganda');
    },
        5000);
});