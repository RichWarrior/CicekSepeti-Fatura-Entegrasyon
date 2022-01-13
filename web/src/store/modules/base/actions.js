import { IS_LOADING } from './actions.type'
import { CHANGE_LOADING } from './mutations.type'

const actions = {
    [IS_LOADING](context) {
        context.commit(CHANGE_LOADING)
    }
}

export default actions;