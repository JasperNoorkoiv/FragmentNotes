﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace FragmentSample
{
    public class TitlesFragment : ListFragment
    {
        int selectedPlayId;
        bool showingTwoFragments;

        public TitlesFragment()
        {

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            var DatabaseHelper = new DatabaseHelper();
            var titles = DatabaseHelper.GetAllNotes().ToList().Select(p => p.Title).ToArray();
            base.OnActivityCreated(savedInstanceState);
            ListAdapter = new ArrayAdapter<string>(Activity,
                Android.Resource.Layout.SimpleListItemActivated1, titles);
            

            if(savedInstanceState != null)
            {
                selectedPlayId = savedInstanceState.GetInt("current_play_id", 0);
            }

            var quoteContainer = Activity.FindViewById(Resource.Id.playnote_container);
            showingTwoFragments = quoteContainer != null && 
                quoteContainer.Visibility == ViewStates.Visible;

            if (showingTwoFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                ShowPlayQuote(selectedPlayId);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_play_id", selectedPlayId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowPlayQuote(position);
        }

        private void ShowPlayQuote(int playId)
        {
            selectedPlayId = playId;
            if(showingTwoFragments)
            {
                ListView.SetItemChecked(selectedPlayId, true);
                var playQuoteFragment = FragmentManager.FindFragmentById(Resource.Id.playnote_container)
                    as PlayQuoteFragment;

                if(playQuoteFragment == null || playQuoteFragment.NoteId != playId )
                {

                    var container = Activity.FindViewById(Resource.Id.playnote_container);
                    var quoteFrag = PlayQuoteFragment.NewInstance(selectedPlayId);

                    FragmentTransaction ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.playnote_container, quoteFrag);
                    ft.Commit();
                }
            }
            else
            {
                var intent = new Intent(Activity, typeof(PlayQuoteActivity));
                intent.PutExtra("current_play_id", playId);
                StartActivity(intent);
            }           
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}