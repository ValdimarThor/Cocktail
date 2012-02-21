//====================================================================================================================
// Copyright (c) 2012 IdeaBlade
//====================================================================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//====================================================================================================================
// USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
// http://cocktail.ideablade.com/licensing
//====================================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Cocktail;
using Common.Errors;
using Common.Factories;
using Common.Repositories;
using DomainModel;
using IdeaBlade.Core;

namespace TempHire.ViewModels.StaffingResource
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffingResourceAddressListViewModel : StaffingResourceScreenBase
    {
        private readonly IPartFactory<AddressTypeSelectorViewModel> _addressTypeSelectorFactory;
        private readonly IDialogManager _dialogManager;
        private readonly IRepositoryManager<IStaffingResourceRepository> _repositoryManager;
        private BindableCollection<StaffingResourceAddressItemViewModel> _addresses;
        private BindableCollection<State> _states;

        [ImportingConstructor]
        public StaffingResourceAddressListViewModel(IRepositoryManager<IStaffingResourceRepository> repositoryManager,
                                            IPartFactory<AddressTypeSelectorViewModel> addressTypeSelectorFactory,
                                            IErrorHandler errorHandler, IDialogManager dialogManager)
            : base(repositoryManager, errorHandler)
        {
            _repositoryManager = repositoryManager;
            _addressTypeSelectorFactory = addressTypeSelectorFactory;
            _dialogManager = dialogManager;
        }

        public override DomainModel.StaffingResource StaffingResource
        {
            get { return base.StaffingResource; }
            set
            {
                if (base.StaffingResource != null)
                    base.StaffingResource.Addresses.CollectionChanged -= AddressesCollectionChanged;

                ClearAddresses();

                if (value != null)
                {
                    Addresses =
                        new BindableCollection<StaffingResourceAddressItemViewModel>(
                            value.Addresses.ToList().Select(a => new StaffingResourceAddressItemViewModel(a)));
                    value.Addresses.CollectionChanged += AddressesCollectionChanged;
                }

                base.StaffingResource = value;
            }
        }

        public BindableCollection<StaffingResourceAddressItemViewModel> Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value;
                NotifyOfPropertyChange(() => Addresses);
            }
        }

        public BindableCollection<State> States
        {
            get { return _states; }
            private set
            {
                _states = value;
                NotifyOfPropertyChange(() => States);
            }
        }

        public override StaffingResourceScreenBase Start(Guid staffingResourceId)
        {
            StartCore(staffingResourceId).ToSequentialResult().Execute();

            return this;
        }

        private IEnumerable<IResult> StartCore(Guid staffingResourceId)
        {
            // Load the list of states once first, before we continue with starting the ViewModel
            // This is to ensure that the combobox binding doesn't goof up if the ItemSource is empty
            // The list of states is preloaded into each EntityManager cache, so this should be fast
            if (States == null)
            {
                IStaffingResourceRepository repository = _repositoryManager.GetRepository(staffingResourceId);
                yield return repository.GetStatesAsync(result => States = new BindableCollection<State>(result),
                                                       ErrorHandler.HandleError);
            }

            base.Start(staffingResourceId);
        }

        private void AddressesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (StaffingResourceAddressItemViewModel item in
                    e.OldItems.Cast<Address>().Select(a => Addresses.First(i => i.Item == a)))
                {
                    Addresses.Remove(item);
                    item.Dispose();
                }
            }

            if (e.NewItems != null)
                e.NewItems.Cast<Address>()
                    .ForEach(a => Addresses.Add(new StaffingResourceAddressItemViewModel(a)));

            EnsureDelete();
        }

        private void EnsureDelete()
        {
            Addresses.ForEach(i => i.NotifyOfPropertyChange(() => i.CanDelete));
        }

        public IEnumerable<IResult> Add()
        {
            AddressTypeSelectorViewModel addressTypeSelector = _addressTypeSelectorFactory.CreatePart();
            yield return _dialogManager.ShowDialog(addressTypeSelector.Start(StaffingResource.Id), DialogButtons.OkCancel);

            StaffingResource.AddAddress(addressTypeSelector.SelectedAddressType);

            EnsureDelete();
        }

        public void Delete(StaffingResourceAddressItemViewModel addressItem)
        {
            StaffingResource.DeleteAddress(addressItem.Item);

            EnsureDelete();
        }

        public void SetPrimary(StaffingResourceAddressItemViewModel addressItem)
        {
            StaffingResource.PrimaryAddress = addressItem.Item;
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (!close) return;

            ClearAddresses();
        }

        private void ClearAddresses()
        {
            if (Addresses == null) return;

            // Clean up to avoid memory leaks
            Addresses.ForEach(i => i.Dispose());
            Addresses.Clear();
        }
    }
}