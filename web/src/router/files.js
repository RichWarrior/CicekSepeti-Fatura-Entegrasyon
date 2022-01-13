export default {
    path: '/files',
    component: () =>
        import ('../layout/DashboardLayout.vue'),
    children: [{
            path: '',
            name: 'files',
            component: () =>
                import ('../views/Files/index.vue')
        },
        {
            path: 'list',
            name: 'fileList',
            component: () =>
                import ('../views/Files/index.vue')
        },
        {
            path: 'view/:id',
            name: 'viewFile',
            component: () =>
                import ('../views/Files/view.vue')
        },
    ]
}