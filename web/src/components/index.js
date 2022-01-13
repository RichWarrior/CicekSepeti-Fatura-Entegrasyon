export const Setup = (vue) => {
    vue.component('ciceksepeti-toolbar', () =>
        import ('./Toolbar.vue'))

    vue.component('ciceksepeti-sidebar', () =>
        import ('./Sidebar.vue'))
}