﻿namespace DevExpress.OutlookInspiredApp.Win.Modules {
    using DevExpress.OutlookInspiredApp.Win.Presenters;
    using DevExpress.OutlookInspiredApp.Win.ViewModel;
    using DevExpress.XtraLayout.Utils;

    public partial class QuotesFilterPaneCollapsed : BaseModuleControl, ISupportCompactLayout {
        QuoteFilterPanePresenter presenterCore;
        public QuotesFilterPaneCollapsed(QuoteCollectionViewModel collectionViewModel)
            : base(() => CreateViewModel(() => new QuotesFilterTreeViewModel(collectionViewModel))) {
            InitializeComponent();
            presenterCore = CreatePresenter();
            BindCommands();
        }
        protected override void OnDisposing() {
            Presenter.Dispose();
            base.OnDisposing();
        }
        public QuotesFilterTreeViewModel ViewModel {
            get { return GetViewModel<QuotesFilterTreeViewModel>(); }
        }
        public QuoteFilterPanePresenter Presenter {
            get { return presenterCore; }
        }
        protected virtual QuoteFilterPanePresenter CreatePresenter() {
            return new QuoteFilterPanePresenter(navigationBar, ViewModel);
        }
        protected virtual void BindCommands() {
            btnNew.BindCommand(() => Presenter.CollectionViewModel.New(), Presenter.CollectionViewModel);
        }
        #region ISupportCompactLayout Members
        bool compactLayout = true;
        bool ISupportCompactLayout.Compact {
            get { return compactLayout; }
            set {
                if(compactLayout == value) return;
                compactLayout = value;
                UpdateCompactLayout();
            }
        }
        void UpdateCompactLayout() {
            btnNewLayoutControlItem.Visibility = compactLayout ? LayoutVisibility.Never : LayoutVisibility.Always;
        }
        #endregion
    }
}
