import { CHANGE_LOADING } from './mutations.type'

const mutations = {
    [CHANGE_LOADING](state) {
        state.isLoading = !state.isLoading
    }
}

export default mutations;