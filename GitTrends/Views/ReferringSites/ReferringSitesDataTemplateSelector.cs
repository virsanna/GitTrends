﻿using FFImageLoading.Forms;
using GitTrends.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.PancakeView;
using static GitTrends.XamarinFormsService;
using static Xamarin.Forms.Markup.GridRowsColumns;

namespace GitTrends
{
    class ReferringSitesDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) => new ReferringSitesDataTemplate((MobileReferringSiteModel)item);

        class ReferringSitesDataTemplate : DataTemplate
        {
            public ReferringSitesDataTemplate(MobileReferringSiteModel referringSiteModel) : base(() => CreateReferringSitesDataTemplate(referringSiteModel))
            {
            }

            enum Row { Title, Description }
            enum Column { FavIcon, FavIconPadding, Site, Referrals, ReferralPadding, Separator, UniquePadding, Uniques }

            static View CreateReferringSitesDataTemplate(in MobileReferringSiteModel referringSiteModel) => new CardView
            {
                Content = new Grid
                {

                    RowDefinitions = Rows.Define(
                        (Row.Title, StarGridLength(1)),
                        (Row.Description, StarGridLength(1))),

                    ColumnDefinitions = Columns.Define(
                        (Column.FavIcon, AbsoluteGridLength(32)),
                        (Column.FavIconPadding, AbsoluteGridLength(16)),
                        (Column.Site, StarGridLength(1)),
                        (Column.Referrals, Auto),
                        (Column.ReferralPadding, AbsoluteGridLength(4)),
                        (Column.Separator, AbsoluteGridLength(1)),
                        (Column.UniquePadding, AbsoluteGridLength(4)),
                        (Column.Uniques, Auto)),

                    Children =
                    {
                        new CircleBoxView(){ Content = new FavIconImage(referringSiteModel.FavIcon) }.Row(Row.Title).Column(Column.FavIcon).RowSpan(2),
                        new TitleLabel("SITE").Row(Row.Title).Column(Column.Site).Start().Margin(new Thickness(0,0,16,0)),
                        new PrimaryColorLabel(referringSiteModel.Referrer).Row(Row.Description).Column(Column.Site).Start().Margin(new Thickness(0,0,16,0)),
                        new TitleLabel("REFERRALS").Row(Row.Title).Column(Column.Referrals).Center(),
                        new StatisticsLabel(12, referringSiteModel.TotalCount, nameof(BaseTheme.PrimaryTextColor), FontFamilyConstants.RobotoRegular).Row(Row.Description).Column(Column.Referrals).Center(),
                        new Separator().Row(Row.Title).Column(Column.Separator).RowSpan(2).FillExpandVertical(),
                        new TitleLabel("UNIQUE").Row(Row.Title).Column(Column.Uniques).Center(),
                        new StatisticsLabel(12, referringSiteModel.TotalUniqueCount, nameof(BaseTheme.PrimaryTextColor), FontFamilyConstants.RobotoRegular).Row(Row.Description).Column(Column.Uniques).Center()
                    }
                }
            };

            class CardView : PancakeView
            {
                public CardView()
                {
                    BorderThickness = 2;
                    CornerRadius = 4;
                    HasShadow = false;
                    Visual = VisualMarker.Material;
                    Padding = new Thickness(16);

                    SetDynamicResource(BorderColorProperty, nameof(BaseTheme.CardBorderColor));
                    SetDynamicResource(BackgroundColorProperty, nameof(BaseTheme.CardSurfaceColor));
                }
            }

            class FavIconImage : CachedImage
            {
                public FavIconImage(Xamarin.Forms.ImageSource imageSource)
                {
                    WidthRequest = HeightRequest = MobileReferringSiteModel.FavIconSize;
                    HorizontalOptions = VerticalOptions = LayoutOptions.Center;
                    LoadingPlaceholder = FavIconService.DefaultFavIcon;
                    ErrorPlaceholder = FavIconService.DefaultFavIcon;
                    Source = imageSource;
                }
            }

            class TitleLabel : PrimaryColorLabel
            {

                public TitleLabel(in string text) : base(text)
                {
                    CharacterSpacing = 1.56;
                    HorizontalTextAlignment = TextAlignment.Start;
                    VerticalTextAlignment = TextAlignment.Start;
                    FontFamily = FontFamilyConstants.RobotoMedium;

                    SetDynamicResource(TextColorProperty, nameof(BaseTheme.TextColor));
                }
            }

            class PrimaryColorLabel : Label
            {
                public PrimaryColorLabel(in double fontSize, in string text) : this(text) => FontSize = fontSize;

                public PrimaryColorLabel(in string text)
                {
                    Text = text;
                    FontSize = 12;
                    MaxLines = 1;
                    LineBreakMode = LineBreakMode.TailTruncation;
                    HorizontalTextAlignment = TextAlignment.Start;
                    HorizontalOptions = LayoutOptions.FillAndExpand;
                    FontFamily = FontFamilyConstants.RobotoRegular;

                    SetDynamicResource(TextColorProperty, nameof(BaseTheme.PrimaryTextColor));
                }
            }

            class Separator : BoxView
            {
                public Separator()
                {
                    SetDynamicResource(ColorProperty, nameof(BaseTheme.SeparatorColor));
                }
            }

            class CircleBoxView : Frame
            {
                public CircleBoxView()
                {
                    Visual = VisualMarker.Material;
                    HeightRequest = 32;
                    WidthRequest = 32;
                    HorizontalOptions = VerticalOptions = LayoutOptions.Start;
                    CornerRadius = 18;
                    HasShadow = false;
                    Padding = new Thickness(0);
                    BackgroundColor = Color.White;
                }
            }
        }
    }
}