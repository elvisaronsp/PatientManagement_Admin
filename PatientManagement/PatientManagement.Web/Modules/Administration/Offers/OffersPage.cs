﻿
using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;
using PatientManagement.Administration.Entities;
using PatientManagement.Administration.Repositories;
using Serenity.Data;
using Serenity.Services;

namespace PatientManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;


    [PageAuthorize("Administration:Offers:Page")]
    public class OffersController : Controller
    {
        [Route("Administration/Offers")]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/Offers/OffersIndex.cshtml");
        }
    }


    public class OffersPublicController : Controller
    {
        [Route("api/Offers/{currencyCode}")]
        public JsonResult Index(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return new JsonResult(NotFound());

            currencyCode = currencyCode.ToUpper();
            var model = new List<OffersPublicModel>();
            using (var connection = SqlConnections.NewFor<OffersRow>())
            {
                var currencyFields = CurrenciesRow.Fields;
                if (!connection.Exists<CurrenciesRow>(currencyFields.CurrencyId == currencyCode))
                {
                    return new JsonResult(NotFound());
                }

                var offerFields = OffersRow.Fields;
                var offers = connection.List<OffersRow>()
                    .Select(x => new { x.OfferId, x.Name, x.Price, x.CurrencyId });

                foreach (var offer in offers)
                {
                    var currencyOffer = connection.First<CurrenciesRow>(currencyFields.Id == offer.CurrencyId.Value);

                    var neededCurrency = connection.First<CurrenciesRow>(currencyFields.CurrencyId == currencyCode);

                    if (offer.CurrencyId == neededCurrency.Id)
                    {
                        model.Add(new OffersPublicModel
                        {
                            OfferId = offer.OfferId ?? 0,
                            OfferName = offer.Name,
                            Price = new Money(offer.Price ?? 0, Currency.FromCode(currencyOffer.CurrencyId)).ToString()
                        });
                    }
                    else
                    {
                        ExchangeRate exchangeRateOffer;

                        if (currencyOffer.BaseCurrencyId.HasValue)
                        {
                            var baseCurrency =
                                connection.First<CurrenciesRow>(
                                    currencyFields.Id == currencyOffer.BaseCurrencyId.Value);

                            exchangeRateOffer = new ExchangeRate(Currency.FromCode(baseCurrency.CurrencyId),
                                Currency.FromCode(currencyOffer.CurrencyId),
                                currencyOffer.Rate ?? 0);

                            if (baseCurrency.CurrencyId == currencyCode)
                            {
                                model.Add(new OffersPublicModel
                                {
                                    OfferId = offer.OfferId ?? 0,
                                    OfferName = offer.Name,
                                    Price = exchangeRateOffer
                                        .Convert(new Money(offer.Price ?? 0,
                                            Currency.FromCode(currencyOffer.CurrencyId)))
                                        .ToString()
                                });
                            }
                            else
                            {
                                var tempPriceOffer = exchangeRateOffer.Convert(
                                    new Money(offer.Price ?? 0, Currency.FromCode(currencyOffer.CurrencyId)));

                                var exchangeRateNeeded = new ExchangeRate(Currency.FromCode(baseCurrency.CurrencyId),
                                    Currency.FromCode(neededCurrency.CurrencyId), neededCurrency.Rate ?? 0);
                                model.Add(new OffersPublicModel
                                {
                                    OfferId = offer.OfferId ?? 0,
                                    OfferName = offer.Name,
                                    Price = exchangeRateNeeded.Convert(tempPriceOffer).ToString()
                                });
                            }
                        }
                        else
                        {
                            var exchangeRateNeeded = new ExchangeRate(Currency.FromCode(currencyOffer.CurrencyId),
                                Currency.FromCode(neededCurrency.CurrencyId), neededCurrency.Rate ?? 0);

                            model.Add(new OffersPublicModel
                            {
                                OfferId = offer.OfferId ?? 0,
                                OfferName = offer.Name,
                                Price = exchangeRateNeeded.Convert(new Money(offer.Price ?? 0, Currency.FromCode(currencyOffer.CurrencyId))).ToString()
                            });
                        }

                    }
                }



            }

            return new JsonResult(model);
        }

        public class OffersPublicModel
        {
            public Int32 OfferId { get; set; }

            public string OfferName { get; set; }

            public string Price { get; set; }

        }
    }
}