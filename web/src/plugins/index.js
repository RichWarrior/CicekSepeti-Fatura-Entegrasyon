import httpClient from '../common/HttpClient'

import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import currencyFilter from '../filters/CurrencyFilter'

export const Setup = vue => {
    httpClient.init();
    currencyFilter.init(vue)

    vue.use(VueSweetalert2)
    vue.use(require('vue-moment'));
}