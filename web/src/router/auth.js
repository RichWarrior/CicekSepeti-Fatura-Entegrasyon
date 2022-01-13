export default {
    path: '/auth',
    component: () =>
        import ('../layout/BlankLayout.vue'),
    children: [{
            path: '',
            name: 'blankLogin',
            component: () =>
                import ('../views/Auth/login.vue')
        },
        {
            path: 'login',
            name: 'login',
            component: () =>
                import ('../views/Auth/login.vue')
        }
    ]
}