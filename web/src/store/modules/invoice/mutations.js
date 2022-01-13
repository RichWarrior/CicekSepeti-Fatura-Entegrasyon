import { SET_INVOICES } from './mutations.type'

const mutations = {
    [SET_INVOICES](state, payload) {
        state.invoices = payload.invoices;
    }
}

export default mutations;