﻿namespace DevExpress.OutlookInspiredApp.Win.Modules {
    using DevExpress.DevAV;
    using DevExpress.OutlookInspiredApp.Win.ViewModel;

    public partial class QuotesCustomFilter : BaseModuleControl {
        public QuotesCustomFilter(CustomFilterViewModel customFilterViewModel)
            : base(() => customFilterViewModel) {
            InitializeComponent();
            ViewModel.QueryFilterCriteria += ViewModel_QueryFilterCriteria;
            bindingSource.DataSource = customFilterViewModel;
            BuildFilterColumns();
            BindEditors();
            BindCommands();
        }
        protected override void OnDisposing() {
            ViewModel.QueryFilterCriteria -= ViewModel_QueryFilterCriteria;
            base.OnDisposing();
        }
        protected override void OnLoad(System.EventArgs ea) {
            base.OnLoad(ea);
            filterControl.FilterCriteria = ViewModel.FilterCriteria;
        }
        void ViewModel_QueryFilterCriteria(object sender, QueryFilterCriteriaEventArgs e) {
            e.FilterCriteria = filterControl.FilterCriteria;
        }
        public CustomFilterViewModel ViewModel {
            get { return GetViewModel<CustomFilterViewModel>(); }
        }
        public QuoteCollectionViewModel CollectionViewModel {
            get { return GetParentViewModel<QuoteCollectionViewModel>(); }
        }
        void BuildFilterColumns() {
            var builder = new FilterColumnCollectionBuilder<Quote>(filterControl.FilterColumns);
            builder
                .AddColumn(e => e.Customer)
                .AddColumn(e => e.CustomerStore)
                .AddDateTimeColumn(e => e.Date)
                .AddColumn(e => e.Employee)
                .AddColumn(e => e.Number)
                .AddColumn(e => e.Opportunity)
                .AddColumn(e => e.ShippingAmount)
                .AddColumn(e => e.SubTotal)
                .AddColumn(e => e.Total);
        }
        void BindEditors() {
            var errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            errorProvider.ContainerControl = this;
            errorProvider.DataSource = bindingSource;
        }
        void BindCommands() {
            this.okBtn.BindCommand(() => ViewModel.OK(), ViewModel);
            this.cancelBtn.BindCommand(() => ViewModel.Cancel(), ViewModel);
        }
    }
}
