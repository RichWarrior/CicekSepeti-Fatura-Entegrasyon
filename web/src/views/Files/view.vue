<template>
  <v-row class="ma-2">
    <v-col cols="12" class="pa-2">
      <v-card>
        <v-toolbar class="gradient" dark>
          <h1 class="title">Evrak Detayı</h1>
          <v-spacer></v-spacer>
          <v-btn icon @click="processItems">
            <v-icon>fa fa-check-circle</v-icon>
          </v-btn>
        </v-toolbar>
        <v-card-text>
          <v-data-table
            v-model="selectedInvoices"
            :headers="headers"
            :items="invoices"
            class="elevation-1"
            :items-per-page="100000"
            hide-default-footer
            :search="search"
            :custom-filter="searchItem"
            show-select
          >
          <template v-slot:item.price="{item}">
            {{item.price | toCurrency}}
          </template>
          <template v-slot:item.tax="{item}">
            {{item.tax | toCurrency}}
          </template>
          <template v-slot:item.subTotal="{item}">
            {{item.subTotal | toCurrency}}
          </template>
            <template v-slot:top>
              <v-row>
                <v-col cols="12" md="6">
                  <v-select
                    v-model="selectedProcessType"
                    outlined
                    class="pa-2"
                    label="İşlem Seçiniz"
                    :items="processTypes"
                    item-text="text"
                    item-value="value"
                  ></v-select>
                </v-col>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="search"
                    label="Ara"
                    outlined
                    class="pa-2"
                  ></v-text-field>
                </v-col>
              </v-row>
            </template>
            <template v-slot:item.invoiceDate="{ item }">
              {{ $moment(item.invoiceDate).format("DD/MM/YYYY") }}
            </template>
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import listEntity from "../../dto/request/invoice/List";
import processEntity from "../../dto/request/invoice/Process";
import {
  GET_INVOICES,
  PROCESS_INVOCES,
} from "../../store/modules/invoice/actions.type";
import { ShowErrorMessage, ShowSuccessMessage } from "../../common/Alerts";
export default {
  data() {
    return {
      search: "",
      headers: [
        {
          text: "Sipariş No",
          align: "start",
          sortable: true,
          value: "orderId",
        },
        {
          text: "Alt Sipariş No",
          align: "start",
          sortable: true,
          value: "subOrderId",
        },
        {
          text: "Fatura Id",
          align: "start",
          sortable: true,
          value: "invoiceUniqueKey",
        },
        {
          text: "Ürün Adı",
          align: "start",
          sortable: true,
          value: "productName",
        },
        {
          text: "Ürün Varyant Adı",
          align: "start",
          sortable: true,
          value: "productSecondName",
        },
        {
          text: "Adet",
          align: "start",
          sortable: true,
          value: "piece",
        },
        {
          text: "Birim Fiyat",
          align: "start",
          sortable: true,
          value: "price",
        },
        {
          text: "KDV",
          align: "start",
          sortable: true,
          value: "tax",
        },
        {
          text: "KDV Oranı",
          align: "start",
          sortable: true,
          value: "taxRate",
        },
        {
          text: "Toplam",
          align: "start",
          sortable: true,
          value: "subTotal",
        },
        {
          text: "Müşteri",
          align: "start",
          sortable: true,
          value: "customerName",
        },
        {
          text: "Vergi No",
          align: "start",
          sortable: true,
          value: "customerVKN",
        },
        {
          text:'Vergi Dairesi',
          align:'start',
          sortable:true,
          value:'taxOffice'
        },
        {
          text:'Adres',
          align:'start',
          sortable:true,
          value:'address'
        },
        {
          text: "Fatura Tarihi",
          align: "start",
          sortable: true,
          value: "invoiceDate",
        },
        {
          text: "Durum",
          align: "start",
          sortable: true,
          value: "invoiceStatus",
        },
      ],
      invoices: [],
      selectedInvoices: [],
      processTypes: [
        {
          text: "Taslak Oluşturmayı Bekleniyor",
          value: "1",
        },
        {
          text: "Taslak Oluşturuldu",
          value: "3",
        },
      ],
      selectedProcessType: 0,
    };
  },
  methods: {
    getInvoices() {
      var dto = new listEntity();
      dto.Id = parseInt(this.$route.params.id);
      this.$store
        .dispatch(GET_INVOICES, dto)
        .then(() => {
          this.invoices = this.$store.getters.getInvoices;
        })
        .catch((err) => {
          ShowErrorMessage(err.message);
        });
    },
    searchItem(value, search) {
      return (
        value != null &&
        search != null &&
        typeof value === "string" &&
        value.toString().indexOf(search) !== -1
      );
    },
    processItems() {
      if (this.selectedProcessType < 1) {
        ShowErrorMessage("İşlem Tipi Seçiniz");
        return;
      }
      if (this.selectedInvoices.length < 1) {
        ShowErrorMessage("Fatura Seçiniz");
        return;
      }

      var selectedInvoiceIds = this.selectedInvoices.map((f) => {
        return f.id;
      });

      var dto = new processEntity();
      dto.Invoices = selectedInvoiceIds;
      dto.InvoiceStatusId = this.selectedProcessType;
      this.$store
        .dispatch(PROCESS_INVOCES, dto)
        .then((payload) => {
          ShowSuccessMessage(payload.message);
        })
        .catch((err) => {
          ShowErrorMessage(err.message);
        });
    },
  },
  created() {
    this.getInvoices();
  },
};
</script>