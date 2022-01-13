const CurrencyFilter = {
    init(Vue) {
        Vue.filter('toCurrency', function(value) {
            if (typeof value !== "number") {
                return value;
            }
            var formatter = new Intl.NumberFormat('tr-TR', {
                style: 'currency',
                currency: 'TRY'
            });
            return formatter.format(value);
        });
    }
}

export default CurrencyFilter;