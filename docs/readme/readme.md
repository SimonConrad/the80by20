## Event Storming model
https://miro.com/app/board/uXjVOkhNSOs=/


## todos
TODO Comment each ddd tactical element with practilal explanantion - when to use, what gives us
todo fix typos
todo review and fix content
todo use for slides in presentations


## bounded context
domeny, subdomeny, b ctxty, archetypy modeli biznesowy, struktury wielkiej skali

najpierw subdomeny, potem destylacja b ctxtu
granice bounded contextu wyznaczemy heurystyka pojedynczego zrodla prawdy, czyli jest jedno zrodlo prawdy jesli:
zadajac pytanie biznesowe otrzymuje odpowiedz, na ktora wplyw maja komendy + zdarzenia wystepujace w ramach jednego kontkstu
np złamanie zasady pojedynczego zrodla prawdy:
jesli zadajac pytanie o dostepnosc musze wiedziec czy jest cos na magazynie (1 b.ctxt)
+ dodtakowo złozyć to z inromacja czy cos jest w rezerwacji (2 ctxt)
powinienem miec jeden bctxt dostepnosc, część wspólna to id - wzorzec snowflake i to, że produkt jest czym innym w bctxt dostepnosc a czym innym w bctxt bestseller
z kolei kesz tych informacji readmodel moze byc zlozony z kilku bctxtow, ale to kesz - wtorny wzgledem komand ktore wplywaja na informacje czyli odpowiedz na pytanie biznesowe
te komendy i zdarzenia powinny byc razem w jednym bctxt

inne złamanie pojedynczego źrdóła prawy żeby  na pytanie czy pacjent jes żywy - musimy przejśc sie po kilku oddziałącj i zajc do kostnicy
antywzorzec feature envy - jeden bctxt zadzrosci drugiemy danych, zachowań
ksiazki veinberga

## messaging
https://bd90.pl/mediatr-wprowadzenie-eventow-do-swiata-net-core/

