<template>
  <v-row class="ma-2">
    <v-col cols="12" class="pa-2">
      <v-card>
        <v-toolbar class="gradient" dark>
          <h1 class="title">Evraklarım</h1>
          <v-spacer></v-spacer>
          <v-btn icon @click="dialog = true">
            <v-icon>fa fa-upload</v-icon>
          </v-btn>
        </v-toolbar>

        <v-card-text>
          <v-data-table
            :headers="headers"
            :items="files"
            class="elevation-1"
            :items-per-page="100000"
            hide-default-footer
          >
            <template v-slot:item.totalPrice="{ item }">
              {{ item.totalPrice | toCurrency }}
            </template>
            <template v-slot:item.totalTax="{ item }">
              {{ item.totalTax | toCurrency }}
            </template>
            <template v-slot:item.totalSubTotal="{ item }">
              {{ item.totalSubTotal | toCurrency }}
            </template>
            <template v-slot:item.termStartDate="{ item }">
              {{ $moment(item.termStartDate).format("DD/MM/YYYY") }}
            </template>
            <template v-slot:item.termEndDate="{ item }">
              {{ $moment(item.termEndDate).format("DD/MM/YYYY") }}
            </template>
            <template v-slot:item.creationDate="{ item }">
              {{ $moment(item.creationDate).format("DD/MM/YYYY hh:mm:ss") }}
            </template>
            <template v-slot:item.actions="{ item }">
              <v-btn icon @click="showFile(item)">
                <v-icon>fa fa-eye</v-icon>
              </v-btn>
              <v-btn icon v-if="item.hasFile" @click="download(item.id)">
                <v-icon>fa fa-download</v-icon>
              </v-btn>
            </template>
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-col>

    <!-- Dialog !-->
    <v-dialog v-model="dialog" persistent max-width="600px">
      <v-card>
        <v-card-title>
          <span class="text-h5">Yeni Evrak Yükle</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form v-model="formValid">
              <v-row>
                <v-col cols="12">
                  <v-menu
                    v-model="termStartMenu"
                    :close-on-content-click="false"
                    :nudge-right="40"
                    transition="scale-transition"
                    offset-y
                    min-width="auto"
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                        v-model="file.TermStartDate"
                        :rules="validations.termStartDate"
                        label="Dönem Başlangıç Tarihi"
                        prepend-icon="mdi-calendar"
                        readonly
                        v-bind="attrs"
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="file.TermStartDate"
                      @input="termStartMenu = false"
                      locale="tr"
                    ></v-date-picker>
                  </v-menu>
                </v-col>
                <v-col cols="12">
                  <v-menu
                    v-model="termEndMenu"
                    :close-on-content-click="false"
                    :nudge-right="40"
                    transition="scale-transition"
                    offset-y
                    min-width="auto"
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                        v-model="file.TermEndDate"
                        :rules="validations.termEndDate"
                        label="Dönem Bitiş Tarihi"
                        prepend-icon="mdi-calendar"
                        readonly
                        v-bind="attrs"
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="file.TermEndDate"
                      @input="termEndMenu = false"
                      locale="tr"
                    ></v-date-picker>
                  </v-menu>
                </v-col>
                <v-col cols="12">
                  <v-file-input
                    v-model="file.File"
                    :rules="validations.file"
                    accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    label="Excel Dosyası"
                  ></v-file-input>
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="dialog = false"> Kapat </v-btn>
          <v-btn color="primary" text :disabled="!formValid" @click="upload">
            Kaydet
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Dialog !-->
  </v-row>
</template>

<script>
import fileEntity from "../../dto/request/files/Upload";
import {
  UPLOAD_FILE,
  GET_FILES,
  DOWNLOAD_FILE,
} from "../../store/modules/file/actions.type";
import { ShowSuccessMessage, ShowErrorMessage } from "../../common/Alerts";
export default {
  data() {
    return {
      formValid: false,
      file: Object.assign({}, new fileEntity()),
      termStartMenu: false,
      termEndMenu: false,
      dialog: false,
      validations: {
        termStartDate: [(v) => !!v || "Bu Alan Gereklidir"],
        termEndDate: [(v) => !!v || "Bu Alan Gereklidir"],
        file: [(value) => !value || value.size > 0 || "Bu Alan Gereklidir"],
      },
      headers: [
        {
          text: "Dosya Adı",
          align: "start",
          sortable: true,
          value: "name",
        },
        { text: "Dönem Başlangıç Tarihi", value: "termStartDate" },
        { text: "Dönem Bitiş Tarihi", value: "termEndDate" },
        { text: "İşlenebilir Fatura Sayısı", value: "invoiceCount" },
        { text: "Yükleme Tarihi", value: "creationDate" },
        { text: "Toplam Tutar(Vergi Hariç)", value: "totalPrice" },
        { text: "Vergi", value: "totalTax" },
        { text: "Toplam Tutar(Vergi Dahil)", value: "totalSubTotal" },
        { text: "İşlemler", value: "actions" },
      ],
      files: [],
    };
  },
  methods: {
    upload() {
      this.dialog = false;
      let form = new FormData();
      form.append("TermStartDate", this.file.TermStartDate);
      form.append("TermEndDate", this.file.TermEndDate);
      form.append("File", this.file.File);
      this.$store
        .dispatch(UPLOAD_FILE, form)
        .then((payload) => {
          ShowSuccessMessage(payload.message);
          this.getFiles();
        })
        .catch((err) => {
          ShowErrorMessage(err.message);
        })
        .finally(() => {
          this.file = Object.assign({}, new fileEntity());
        });
    },
    getFiles() {
      this.$store
        .dispatch(GET_FILES)
        .then(() => {
          this.files = this.$store.getters.getFiles;
        })
        .catch((err) => {
          ShowErrorMessage(err.message);
        });
    },
    showFile(file) {
      this.$router.push({
        name: "viewFile",
        params: {
          id: file.id,
        },
      });
    },
    download(id) {
      var req = {
        id: id,
      };
      this.$store
        .dispatch(DOWNLOAD_FILE, req)
        .then((payload) => {
          var el = document.createElement('a');
          el.href = payload.url;
          el.target = '_blank';
          el.click();
        })
        .catch((err) => {
          ShowErrorMessage(err.message);
        });
    },
  },
  created() {
    this.getFiles();
  },
};
</script>