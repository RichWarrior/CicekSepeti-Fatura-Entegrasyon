import Vue from 'vue'
import Vuex from 'vuex'

import base from './modules/base/'
import auth from './modules/auth/'
import file from './modules/file/'
import invoice from './modules/invoice/'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        base,
        auth,
        file,
        invoice
    }
})