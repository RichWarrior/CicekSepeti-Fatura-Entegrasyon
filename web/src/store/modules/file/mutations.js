import { SET_FILES } from './mutations.type'

const mutations = {
    [SET_FILES](state, payload) {
        state.files = payload.files;
    }
}

export default mutations;