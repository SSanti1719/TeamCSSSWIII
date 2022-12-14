package com.teamcss.jobtime.ui.slideshow;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.teamcss.jobtime.R;
import com.teamcss.jobtime.databinding.FragmentSlideshowBinding;
import com.teamcss.jobtime.helpers.API;
import com.teamcss.jobtime.model.Employee;
import com.teamcss.jobtime.model.Month;
import com.teamcss.jobtime.model.Project;

import java.time.Year;
import java.util.Calendar;

public class SlideshowFragment extends Fragment implements View.OnClickListener {

    private FragmentSlideshowBinding binding;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        SlideshowViewModel slideshowViewModel =
                new ViewModelProvider(this).get(SlideshowViewModel.class);

        binding = FragmentSlideshowBinding.inflate(inflater, container, false);
        binding.spMeses.setAdapter(getMonthList());
        binding.spMeses.setOnItemClickListener((AdapterView.OnItemClickListener) this);

        View root = binding.getRoot();

        //final TextView textView = binding.textSlideshow;
        //slideshowViewModel.getText().observe(getViewLifecycleOwner(), textView::setText);
        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    public ArrayAdapter<Month> getMonthList() {
        ArrayAdapter<Month> months = new ArrayAdapter<Month>(getContext(), android.R.layout.simple_list_item_1);;

        months.add(new Month("1", "Enero"));
        months.add(new Month("2", "Febrero"));
        months.add(new Month("3", "Marzo"));
        months.add(new Month("4", "Abril"));
        months.add(new Month("5", "Mayo"));
        months.add(new Month("6", "Junio"));
        months.add(new Month("7", "Julio"));
        months.add(new Month("8", "Agosto"));
        months.add(new Month("9", "Septiembre"));
        months.add(new Month("10", "Octubre"));
        months.add(new Month("11", "Noviembre"));
        months.add(new Month("12", "Diciembre"));

        return months;
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.spMeses:
                changeMonth();
                break;
        }
    }

    private void changeMonth() {
        Month month = (Month) binding.spMeses.getSelectedItem();
        int year = Calendar.getInstance().get(Calendar.YEAR);
        int numberMonth = Integer.parseInt(month.getId());
        Calendar cal = Calendar.getInstance();
        int dayOfMonth = cal.get(Calendar.DAY_OF_MONTH);
        binding.datePicker1.updateDate(year, numberMonth, dayOfMonth);
    }
}